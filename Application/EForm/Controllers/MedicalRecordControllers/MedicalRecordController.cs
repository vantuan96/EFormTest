using Bussiness.HisService;
using DataAccess.Dapper.Repository;
using DataAccess.Models;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Models.EDModels;
using EForm.Models.IPDModels;
using EForm.Models.MedicalRecordModels;
using EForm.Models.OPDModels;
using EForm.Utils;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace EForm.Controllers.MedicalRecordControllers
{
    [SessionAuthorize]
    public class MedicalRecordController : BaseMedicalRecodeApiController
    {
        private readonly string[] StatusCodes = { "EDIH", "OPDIH", "IPDIH", "EOCIH", "OPDWR", "EDWR", "IPDWR", "EOCWR" };
        [HttpGet]
        [Route("api/MedicalRecord")]
        [Permission(Code = "MMERE1")]
        public IHttpActionResult CustomersAPI([FromUri] CustomerParameterModel request)
        {
            if (string.IsNullOrEmpty(request.Search))
            {
                return Content(HttpStatusCode.OK, new { count = 0, results = new List<dynamic>() });
            }

            var hisCustomers = OHClient.searchPatientByPid(request.Search);

            //if (hisCustomers.Count == 0)
            //{
            //    return Content(HttpStatusCode.OK, new { count = 0, results = new List<dynamic>() });
            //}

            var customerLocal = unitOfWork.CustomerRepository.Find(e => !e.IsDeleted && e.PID == request.Search).FirstOrDefault();

            var his_customer = hisCustomers.FirstOrDefault();
            try
            {
                if (his_customer != null)
                {
                    if (customerLocal != null)
                    {
                        customerLocal = UpdateOHDataForCustomer(customerLocal, MapCustomerInformationFromHIS(his_customer));
                    }
                    else
                    {
                        customerLocal = CreateOHDataForNewCustomer(MapCustomerInformationFromHIS(his_customer));
                    }
                }
            }
            catch (Exception ex)
            {
                CustomLog.intervaljoblog.Info(string.Format("<GET his_customer> Error: {0}", ex));
            }
            if (customerLocal == null) return Content(HttpStatusCode.OK, new { count = 0, results = new List<dynamic>() });
            var customers = new List<Customer>() { customerLocal };

            return Content(HttpStatusCode.OK, new
            {
                count = 1,
                results = customers.Select(e => new
                {
                    e.Id,
                    e.PID,
                    e.Phone,
                    e.Fullname,
                    DateOfBirth = e.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                    CreatedAt = e.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    e.IsVip
                }).ToList()
            });
        }
        [HttpGet]
        [Route("api/MedicalRecord/V2/{PID}")]
        [Permission(Code = "MMERE2")]
        public IHttpActionResult MedicalRecordV2API(string PID)
        {
            var customer = unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.PID) && e.PID == PID);
            if (customer == null)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            var results = GetInfoAllAreaInCustomerId(customer.Id);
            var base_items = new List<VisitGroupInfoModel>();
            base_items = results.Where(e => e.IsTransfer == false).Select(e => new VisitGroupInfoModel()
            {
                Id = (Guid)e.Id,
                AdmittedDate = e.ExaminationTime,
                CreatedAt = e.CreatedAt,
                DischargeDate = e.DischargeDate,
                VisitType = e.Type,
                Status = e.Status,
                StatusCode = e.StatusCode,
                CheckOncology = e.CheckOncology,
                CheckStatus = e.CheckStatus
            }).OrderByDescending(e => e.AdmittedDate).ToList();
            //CheckStatus(results);

            foreach (VisitGroupInfoModel item in base_items)
            {
                var VisitList = getVisitGroupList(item, results);
                var last_item = VisitList.LastOrDefault();
                item.DischargeDate = last_item.DischargeDate;
                item.Status = last_item.Status;
                item.StatusCode = last_item.StatusCode;
                item.VisitList = VisitList;
                item.VisitType = last_item.Type;
                item.PID = PID;
                foreach (var vistitlist in VisitList)
                {
                    var ischeck = IsCheckOncology((Guid)vistitlist.Id);
                    if (ischeck)
                    {
                        item.CheckOncology = "A01_196_050919_V";
                    }
                }
            }
            base_items = base_items.OrderBy(e => e.AdmittedDate).ToList();
            Dictionary<int, List<VisitGroupInfoModel>> listdist = new Dictionary<int, List<VisitGroupInfoModel>>();
            List<VisitGroupInfoModel> listAddProcedure = new List<VisitGroupInfoModel>();
            int indexhas = 0;
            int index = 0;
            foreach (var item in base_items)
            {
                indexhas = indexhas + 1;
                index = index - 1;
                if (item.CheckOncology == "A01_196_050919_V")
                {
                    listAddProcedure.Add(item);
                    if (Constant.CompleteTreatment.Contains(item.StatusCode))
                    {
                        listdist.Add(indexhas, listAddProcedure);
                        listAddProcedure = new List<VisitGroupInfoModel>();
                    }
                    else if (item.Id == base_items.LastOrDefault().Id)
                    {
                        listdist.Add(indexhas, listAddProcedure);
                    }
                    continue;
                }
                else if (listAddProcedure.Count() > 0)
                {
                    listdist.Add(indexhas, listAddProcedure);
                    listAddProcedure = new List<VisitGroupInfoModel>();
                    //
                    listAddProcedure.Add(item);
                    listdist.Add(index, listAddProcedure);
                    listAddProcedure = new List<VisitGroupInfoModel>();
                    continue;
                }

                listAddProcedure.Add(item);
                listdist.Add(index, listAddProcedure);
                listAddProcedure = new List<VisitGroupInfoModel>();
            }
            List<VisitGroupInfoModel> listResult = new List<VisitGroupInfoModel>();
            foreach (var itemPro in listdist)
            {
                if (itemPro.Key > 0)
                {
                    List<VisitModel> visitList = new List<VisitModel>();
                    VisitGroupInfoModel model = itemPro.Value[0];
                    foreach (var kq in itemPro.Value)
                    {
                        if (kq.VisitList != null && kq.VisitList.Any())
                            visitList.AddRange(kq.VisitList);
                    }
                    model.VisitList = visitList;
                    var last_item = visitList.LastOrDefault();
                    model.DischargeDate = last_item.DischargeDate;
                    model.Status = last_item.Status;
                    model.StatusCode = last_item.StatusCode;
                    model.PID = PID;
                    listResult.Add(model);
                }
                else
                {
                    listResult.Add(itemPro.Value[0]);
                }
            }
            listResult = listResult.OrderByDescending(e => e.AdmittedDate).ToList();
            return Content(HttpStatusCode.OK, listResult);
        }

        private List<VisitModel> getVisitGroupList(VisitGroupInfoModel current_item, List<VisitModel> results)
        {
            var visit_list = new List<VisitModel>();
            var first_item = results.FirstOrDefault(e => e.Id == current_item.Id);
            first_item.DiagnosisInfo = GetVisitDiagnosisAndICD((Guid)first_item.Id, first_item.Type, true);
            visit_list.Add(first_item);
            var first_tranform_id = first_item.HandOverCheckListId;
            if (first_tranform_id != null)
            {
                foreach (VisitModel item in results)
                {
                    var finder = results.FirstOrDefault(e => e.TransferFromId != null && e.TransferFromId == first_tranform_id);
                    if (finder != null)
                    {
                        if (new List<string> { "IPD", "ED", "OPD", "EOC" }.Contains(finder.Type))
                        {
                            finder.DiagnosisInfo = GetVisitDiagnosisAndICD((Guid)finder.Id, finder.Type, true);
                        }
                        visit_list.Add(finder);
                        first_tranform_id = finder.HandOverCheckListId;
                        if (first_tranform_id == null) break;
                        continue;
                    }
                }
            }
            return visit_list;
        }

        [HttpGet]
        [Route("api/MedicalRecord/{pid}")]
        [Permission(Code = "MMERE2")]
        public async System.Threading.Tasks.Task<IHttpActionResult> CustomerDetailAPIAsync(string pid, [FromUri] CustomerDetailParameterModel request)
        {
            var customer = unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.PID) && e.PID == pid);
            var all_status = unitOfWork.EDStatusRepository.Find(e => !e.IsDeleted).ToList();
            var no_examination = all_status.FirstOrDefault(e => e.Code == "OPDNE");
            if (customer == null)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            var results = new List<VisitModel>();
            List<string> list_visit_code = new List<string>();

            var eds = getEDVisitByCustomerId(customer.Id);

            list_visit_code.AddRange(eds.Select(e => e.EHOSVisitCode).ToList());
            results.AddRange(eds);

            var opds = getOPDVisitByCustomerId(customer.Id);
            list_visit_code.AddRange(opds.Select(e => e.EHOSVisitCode).ToList());
            results.AddRange(opds);

            var ipds = getIPDVisitByCustomerId(customer.Id);

            list_visit_code.AddRange(ipds.Select(e => e.EHOSVisitCode).ToList());
            results.AddRange(ipds);

            var eocs = getEDCVisitByCustomerId(customer.Id);

            list_visit_code.AddRange(eocs.Select(e => e.EHOSVisitCode).ToList());
            results.AddRange(eocs);
            if (!string.IsNullOrWhiteSpace(customer.PIDEhos) && hasAction("OHMedicalRecord"))
            {
                var ehos = EHosClient.GetVisitHistory(customer.PIDEhos, DateTime.Now);
                results.AddRange(ehos.Where(e => !list_visit_code.Contains(e.EHOSVisitCode)).ToList());
            }

            
            
            if (request.Status != null)
                results = results.Where(e => request.ConvertedStatus.Contains(e.StatusId)).ToList();
                
            if (no_examination != null)
                results = results.Where(e => e.StatusId != no_examination.Id).ToList();

            if (request.VisitCode != null)
                results = results.Where(e => request.ConvertedVisitCode.Contains(e.VisitCode)).ToList();

            if (request.RecordCode != null)
                results = results.Where(e => request.ConvertedRecordCode.Contains(e.RecordCode)).ToList();

            if (request.VisitTypeGroupCode != null)
                results = results.Where(e => request.ConvertedVisitTypeGroupCode.Contains(e.Type)).ToList();

            if (request.Specialty != null)
                results = results.Where(e => request.ConvertedSpecialty.Contains(e.SpecialtyId)).ToList();

            if (request.StartExaminationTime != null && request.EndExaminationTime != null)
                results = results.Where(
                    e => e.ExaminationTime != null &&
                    e.ExaminationTime >= request.ConvertedStartExaminationTime &&
                    e.ExaminationTime <= request.ConvertedEndExaminationTime
                ).ToList();
            else if (request.StartExaminationTime != null)
                results = results.Where(
                    e => e.ExaminationTime != null &&
                    e.ExaminationTime >= request.ConvertedStartExaminationTime
                ).ToList();
            else if (request.EndExaminationTime != null)
                results = results.Where(
                    e => e.ExaminationTime != null &&
                    e.ExaminationTime <= request.ConvertedEndExaminationTime
                ).ToList();

            results = results.OrderByDescending(e => e.ExaminationTime).ToList();
            var count = results.Count();
            //var current_username = getUsername();
            //if (!IsVIPMANAGE() && customer.IsVip)
            //{
            //    results = results.Where(e =>
            //        (e.UnlockFor != null && (e.UnlockFor == "ALL" || ("," + e.UnlockFor + ",").Contains(("," + current_username + ","))))
            //    ).ToList();
            //    if (results.Count() == 0)
            //    {
            //        return Content(HttpStatusCode.Forbidden, new MsgModel()
            //        {
            //            ViMessage = "Bạn không có quyền truy cập hồ sơ này",
            //            EnMessage = "Bạn không có quyền truy cập hồ sơ này"
            //        });
            //    }
            //}

            var visit_types = new List<string> { "ED", "IPD", "OPD", "EOC" };
            dynamic visitId = null;
            dynamic recordCode = null;
            var visitModel = results.Where(e => visit_types.Contains(e.Type)).FirstOrDefault();

            string statuscode = "";
            var emr_visit = results.Where(e => e.Type != "EHOS");
            if (emr_visit != null && emr_visit.Count() > 0)
            {
                var visit = emr_visit.FirstOrDefault();
                visitId = (Guid)visit.Id;
                recordCode = visit.RecordCode;
                statuscode = all_status.FirstOrDefault(e => e.Id == visit.StatusId).Code;
            }
            var gender = new CustomerUtil(customer).GetGender();
            var allergy_info = GetAllergyByVisitModel(visitModel);
            return Content(HttpStatusCode.OK, new
            {
                Customer = new
                {
                    customer.Id,
                    customer.Fullname,
                    customer.PID,
                    DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                    Gender = gender,
                    customer.Nationality,
                    customer.Fork,
                    customer.Job,
                    customer.Phone,
                    customer.Address,
                    customer.IsVip,
                    Age = DateTime.Now.Year - customer.DateOfBirth?.Year,
                    customer.AgeFormated,
                    RelationshipName = customer.Relationship,
                    RelationshipPhone = customer.RelationshipContact,
                    RelationshipAddress = customer.RelationshipAddress,
                    VisitCode = visitModel?.VisitCode,
                    AdmittedDate = visitModel?.ExaminationTime,
                    DischargeDate = StatusCodes.Contains(statuscode) ? null : visitModel?.DischargeDate,
                    Specialty = visitModel?.Specialty,
                    VisitType = visitModel?.Type,
                    DoctorUserName = visitModel?.DoctorUsername,
                    NurseUserName = visitModel?.NurseUsername,
                    Diagnosis = GetAndFormatDiagnosis(visitModel),
                    allergy_info.Allergy,
                    AllergyInfo = allergy_info,
                    VisitId = visitId,
                    RecordCode = recordCode,
                    UserNameReceive = visitModel?.CreatedBy
                },
                Visit = results
            });
        }


        [HttpPost]
        [Route("api/MedicalRecord/UpdateStatus/{VisitId}")]
        [Permission(Code = "ADMINUPDATESTATUS")]
        public IHttpActionResult MedicalRecordUpdateStatus(Guid VisitId, [FromBody] UpdateMedicalRecordStatus request)
        {
            if (request.StatusId == null || string.IsNullOrWhiteSpace(request.Note) || string.IsNullOrWhiteSpace(request.VisitType)) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var stauts = unitOfWork.EDStatusRepository.Find(e => !e.IsDeleted && e.VisitTypeGroup.Code == request.VisitType && e.Id == request.StatusId).FirstOrDefault();
            if (stauts == null) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            Guid? old_status_id;
            switch (request.VisitType)
            {
                case "ED":
                    var ed = GetED(VisitId);
                    old_status_id = ed.EDStatusId;
                    if (ed == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
                    ed.EDStatusId = request.StatusId;
                    unitOfWork.Commit();
                    break;
                case "IPD":
                    var ipd = GetIPD(VisitId);
                    old_status_id = ipd.EDStatusId;
                    if (ipd == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
                    ipd.EDStatusId = request.StatusId;
                    unitOfWork.Commit();
                    break;
                case "OPD":
                    var opd = GetOPD(VisitId);
                    old_status_id = opd.EDStatusId;
                    if (opd == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
                    opd.EDStatusId = request.StatusId;
                    unitOfWork.Commit();
                    break;
                case "EOC":
                    var eoc = GetEOC(VisitId);
                    old_status_id = eoc.StatusId;
                    if (eoc == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
                    eoc.StatusId = request.StatusId;
                    unitOfWork.Commit();
                    break;
                default:
                    break;
            }

            setLog(new Log
            {
                Action = "UPDATE MEDICAL RECORD STATUS",
                URI = VisitId.ToString(),
                Name = "UPDATE MEDICAL RECORD STATUS",
                Request = request.StatusId.ToString(),
                Reason = request.Note,
            });

            return Content(HttpStatusCode.OK, new { });
        }

        private VisitAllergyModel GetAllergyByVisitModel(VisitModel visitMode)
        {
            if (visitMode == null)
                return new VisitAllergyModel() { };

            if (visitMode.CustomerIsAllergy != null)
            {
                return new VisitAllergyModel()
                {
                    IsAllergy = visitMode.CustomerIsAllergy,
                    Allergy = visitMode.CustomerAllergy,
                    KindOfAllergy = visitMode.CustomerKindOfAllergy
                };
            }

            return new VisitAllergyModel() { };
        }

        private string GetAndFormatDiagnosis(VisitModel visit)
        {
            if (visit == null)
                return "";

            StringBuilder builder = new StringBuilder();
            var getcd = GetVisitDiagnosisAndICD((Guid)visit.Id, visit.Type, true);
            builder.Append($"{(string.IsNullOrEmpty(getcd.Diagnosis) ? "" : getcd.Diagnosis)}");
            builder.Append($"{(string.IsNullOrEmpty(getcd.DiagnosisOption) ? "" : (string.IsNullOrEmpty(getcd.Diagnosis) ? getcd.DiagnosisOption : ", " + getcd.DiagnosisOption))}");

            string[] array_Icd = { getcd.ICD, getcd.ICDOption };
            builder.Append(GetAndFormatICD10(array_Icd));

            return builder.ToString();
        }

        private string GetAndFormatICD10(string[] texts)
        {
            string result = String.Empty;

            foreach (var text in texts)
            {
                string str_text = text;
                if (text == null || text == $"\"\"")
                    str_text = "";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                List<Dictionary<string, string>> objs = jss.Deserialize<List<Dictionary<string, string>>>(str_text);
                string _str = String.Empty;
                if (objs != null)
                {
                    int lengthOfobjs = objs.Count;
                    for (int j = 0; j < lengthOfobjs; j++)
                    {
                        var codeIcd10 = objs[j]["code"]?.ToString();
                        if (j == 0)
                            _str += codeIcd10;
                        else
                            _str += $", {codeIcd10}";
                    }

                    if (!string.IsNullOrEmpty(result))
                        result += "/ " + _str;
                    else
                        result += _str;
                }
            }

            if (string.IsNullOrEmpty(result))
                return "";

            string format = $" ({result})";
            return format;
        }

        private string ValidPrimaryDoctor(string username, DateTime? create_time, DateTime? update_time)
        {
            if (create_time.Equals(update_time)) return "";
            return username;
        }
        protected bool IsCheckOncology(Guid visit_id)
        {
            var result = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(e => e.VisitId == visit_id && e.FormCode == "A01_196_050919_V");
            if (result != null)
            {
                return true;
            }
            else
                return false;
        }
        protected List<VisitModel> CheckStatus(List<VisitModel> list)
        {
            foreach (var item in list)
            {
                if (
                    //item.CheckStatus != null &&
                    //(item.CheckStatus.Contains("Dead") ||
                    //item.CheckStatus.Contains("Complete treatment") ||
                    //item.CheckStatus.Contains("Inter-hospital transfer") ||
                    //item.CheckStatus.Contains("Discharged") ||
                    //item.CheckStatus.Contains("No examination") ||
                    //item.CheckStatus.Contains("Upstream/Downstream transfer") ||
                    //item.CheckStatus.Contains("Admitted")) ||
                    Constant.Dead.Contains(item.StatusCode) ||
                    Constant.CompleteTreatment.Contains(item.StatusCode) ||
                    Constant.InterHospitalTransfer.Contains(item.StatusCode) ||
                    Constant.Discharged.Contains(item.StatusCode) ||
                    Constant.NoExamination.Contains(item.StatusCode) ||
                    Constant.UpstreamDownstreamTransfer.Contains(item.StatusCode) ||
                    Constant.Admitted.Contains(item.StatusCode))
                {
                    item.Status = item.Status;
                }
                else
                {
                    item.Status = null;
                }
            }
            return list;
        }

        [HttpPost]
        [Route("api/MedicalRecord/V2")]
        //[Permission(Code = "MMERE1")]
        public IHttpActionResult MedicalRecordV2([FromBody] JObject request)
        {
            Guid Id = Guid.Parse(request["Id"].ToString());
            string VisitType = request["VisitType"].ToString();
            string strCustomerId = "";
            switch (VisitType)
            {
                case "IPD":
                    strCustomerId = GetIPD(Id)?.CustomerId.ToString();
                    break;
                case "ED":
                    strCustomerId = GetED(Id).CustomerId.ToString();
                    break;
                case "OPD":
                    strCustomerId = GetOPD(Id)?.CustomerId.ToString();
                    break;
                case "EOC":
                    strCustomerId = GetEOC(Id)?.CustomerId.ToString();
                    break;
            }
            if (string.IsNullOrEmpty(strCustomerId))
            {
                return Content(HttpStatusCode.NotFound, "Id không tồn tại trên hệ thống");
            }
            var results = GetInfoAllAreaInCustomerId(Guid.Parse(strCustomerId));
            var base_items = new List<VisitGroupInfoModel>();
            base_items = results.Where(e => e.IsTransfer == false).Select(e => new VisitGroupInfoModel()
            {
                Id = (Guid)e.Id,
                AdmittedDate = e.ExaminationTime,
                CreatedAt = e.CreatedAt,
                DischargeDate = e.DischargeDate,
                VisitType = e.Type,
                Status = e.Status,
                StatusCode = e.StatusCode,
                CheckOncology = e.CheckOncology,
                CheckStatus = e.CheckStatus
            }).OrderByDescending(e => e.AdmittedDate).ToList();
            //CheckStatus(results);

            foreach (VisitGroupInfoModel item in base_items)
            {
                var VisitList = getVisitGroupList(item, results);
                var last_item = VisitList.LastOrDefault();
                item.DischargeDate = last_item.DischargeDate;
                item.Status = last_item.Status;
                item.StatusCode = last_item.StatusCode;
                item.VisitList = VisitList;
                foreach (var vistitlist in VisitList)
                {
                    var ischeck = IsCheckOncology((Guid)vistitlist.Id);
                    if (ischeck)
                    {
                        item.CheckOncology = "A01_196_050919_V";
                    }
                }
            }
            base_items = base_items.OrderBy(e => e.AdmittedDate).ToList();
            Dictionary<int, List<VisitGroupInfoModel>> listdist = new Dictionary<int, List<VisitGroupInfoModel>>();
            List<VisitGroupInfoModel> listAddProcedure = new List<VisitGroupInfoModel>();
            int indexhas = 0;
            int index = 0;
            foreach (var item in base_items)
            {
                indexhas = indexhas + 1;
                index = index - 1;
                if (item.CheckOncology == "A01_196_050919_V")
                {
                    listAddProcedure.Add(item);
                    if (item.StatusCode == "IPDCOTM")
                    {
                        listdist.Add(indexhas, listAddProcedure);
                        listAddProcedure = new List<VisitGroupInfoModel>();
                    }
                    else if (item.Id == base_items.LastOrDefault().Id)
                    {
                        listdist.Add(indexhas, listAddProcedure);
                    }
                    continue;
                }
                else if (listAddProcedure.Count() > 0)
                {
                    listdist.Add(indexhas, listAddProcedure);
                    listAddProcedure = new List<VisitGroupInfoModel>();
                    //
                    listAddProcedure.Add(item);
                    listdist.Add(index, listAddProcedure);
                    listAddProcedure = new List<VisitGroupInfoModel>();
                    continue;
                }

                listAddProcedure.Add(item);
                listdist.Add(index, listAddProcedure);
                listAddProcedure = new List<VisitGroupInfoModel>();
            }
            List<VisitGroupInfoModel> listResult = new List<VisitGroupInfoModel>();
             foreach (var itemPro in listdist)
            {
                if (itemPro.Key > 0)
                {
                    List<VisitModel> visitList = new List<VisitModel>();
                    VisitGroupInfoModel model = itemPro.Value[0];
                    foreach (var kq in itemPro.Value)
                    {
                        if (kq.VisitList != null && kq.VisitList.Any())
                            visitList.AddRange(kq.VisitList);
                    }
                    model.VisitList = visitList;
                    var last_item = visitList.LastOrDefault();
                    model.DischargeDate = last_item.DischargeDate;
                    model.Status = last_item.Status;
                    model.StatusCode = last_item.StatusCode;
                    listResult.Add(model);
                }
                else
                {
                    listResult.Add(itemPro.Value[0]);
                }
            }
            listResult = listResult.OrderByDescending(e => e.AdmittedDate).ToList();
            VisitGroupInfoModel visitGroup = null;
            for (int i = 0; i < listResult.Count; i++)
            {
                if (listResult[i].VisitList.LastOrDefault(x => x.Id == Id) != null)
                {
                    visitGroup = listResult[i];
                }    
            }    


            List<MedicalRecordViewTMP> listBA = new List<MedicalRecordViewTMP>();
            List<MedicalRecordViewTMP> listBAOrigin = new List<MedicalRecordViewTMP>();

            var cv = new List<string> { "IPDIHT", "EDTFIH", "EOCIHT", "OPDIHT" };
            var ct = new List<string> { "IPDUDT", "EDUDT", "EOCUD", "OPDUDT" };
            var rv = new List<string> { "IPDDC", "EDDC" };
            var tv = new List<string> { "IPDDD", "EDDD" };
            List<List<VisitModel>> group = new List<List<VisitModel>>();
            if (visitGroup != null)
            {


                List<VisitModel> tmpgroup = new List<VisitModel>();
                foreach (var v in visitGroup.VisitList)
                {
                    // Chuyển viện :IPDIHT,EDTFIH,EOCIHT,OPDIHT
                    // Chuyển tuyến: Upstream/Downstream transfer
                    // Ra viện: Discharged
                    // Tử vong: Dead

                    if (cv.Contains(v.StatusCode)
                        || ct.Contains(v.StatusCode)
                        || rv.Contains(v.StatusCode)
                        || tv.Contains(v.StatusCode)
                        )
                    {
                        tmpgroup.Add(v);
                        group.Add(tmpgroup);
                        tmpgroup = new List<VisitModel>();
                    }
                    else
                    {
                        tmpgroup.Add(v);
                    }

                }
                if (tmpgroup.Count > 0)
                {
                    group.Add(tmpgroup);

                }
                foreach (var visit in visitGroup.VisitList)
                {
                    switch (visit.Type)
                    {
                        case "IPD":
                            if (visit.Id.HasValue)
                            {
                                var ipd = GetIPD(visit.Id.Value);
                                var forms = GetFormsIPD(ipd);
                                var dataInForm = forms.FindAll(x => x.Datas != null && x.Datas.Count > 0).ToList();
                                if (dataInForm.Count > 0)
                                {
                                    listBA.AddRange(dataInForm.Select(x => new MedicalRecordViewTMP
                                    {
                                        Id = visit.Id,
                                        AdmittedDate = ipd?.AdmittedDate,
                                        VisitCode = ipd?.VisitCode,
                                        Specialty = ipd.Specialty?.ViName,
                                        EnSpecialty = ipd.Specialty?.EnName,
                                        PrimaryDoctor = ipd.PrimaryDoctor?.Username,
                                        ViName = x.ViName,
                                        EnName = x.EnName,
                                        Datas = x.Datas,
                                        Type = x.Type,
                                        Code = x.Code,
                                        Area = "IPD",
                                        UpdatedBy = x.UpdatedBy,
                                        UpdatedAt = x.UpdatedAt,
                                        CreatedAt = x.CreatedAt,
                                        StatusCode = ipd.EDStatus.Code,
                                        StatusEnName = ipd.EDStatus.EnName,
                                        StatusViName = ipd.EDStatus.ViName
                                    }));
                                    listBAOrigin.AddRange(dataInForm.Select(x => new MedicalRecordViewTMP
                                    {
                                        Id = visit.Id,
                                        AdmittedDate = ipd?.AdmittedDate,
                                        VisitCode = ipd?.VisitCode,
                                        Specialty = ipd.Specialty?.ViName,
                                        EnSpecialty = ipd.Specialty?.EnName,
                                        PrimaryDoctor = ipd.PrimaryDoctor?.Username,
                                        ViName = x.ViName,
                                        EnName = x.EnName,
                                        Datas = x.Datas,
                                        Type = x.Type,
                                        Code = x.Code,
                                        Area = "IPD",
                                        UpdatedBy = x.UpdatedBy,
                                        UpdatedAt = x.UpdatedAt,
                                        CreatedAt = x.CreatedAt,
                                        StatusCode = ipd.EDStatus.Code,
                                        StatusEnName = ipd.EDStatus.EnName,
                                        StatusViName = ipd.EDStatus.ViName
                                    }));
                                }

                            }

                            break;
                        case "OPD":
                            if (visit.Id.HasValue)
                            {
                                var opd = GetOPD(visit.Id.Value);
                                var forms = GetFormsOPD(opd);
                                var dataInForm = forms.FindAll(x => x.Datas != null && x.Datas.Count > 0).ToList();                                  
                                if (dataInForm.Count > 0)
                                {
                                    listBA.AddRange(dataInForm.Select(x => new MedicalRecordViewTMP
                                    {
                                        Id = visit.Id,
                                        AdmittedDate = opd?.AdmittedDate,
                                        VisitCode = opd?.VisitCode,
                                        Specialty = opd.Specialty?.ViName,
                                        EnSpecialty = opd.Specialty?.EnName,
                                        PrimaryDoctor = opd.PrimaryDoctor?.Username,
                                        ViName = x.ViName,
                                        EnName = x.EnName,
                                        Datas = x.Datas,
                                        Type = x.Type,
                                        Code = x.Code,
                                        Area = "OPD",
                                        UpdatedBy = x.UpdatedBy,
                                        UpdatedAt = x.UpdatedAt,
                                        CreatedAt = x.CreatedAt,
                                        StatusCode = opd.EDStatus.Code,
                                        StatusEnName = opd.EDStatus.EnName,
                                        StatusViName = opd.EDStatus.ViName,
                                        IsAnesthesia = opd.IsAnesthesia
                                    }));
                                    listBAOrigin.AddRange(dataInForm.Select(x => new MedicalRecordViewTMP
                                    {
                                        Id = visit.Id,
                                        AdmittedDate = opd?.AdmittedDate,
                                        VisitCode = opd?.VisitCode,
                                        Specialty = opd.Specialty?.ViName,
                                        EnSpecialty = opd?.Specialty?.EnName,
                                        PrimaryDoctor = opd.PrimaryDoctor?.Username,
                                        ViName = x.ViName,
                                        EnName = x.EnName,
                                        Datas = x.Datas,
                                        Type = x.Type,
                                        Code = x.Code,
                                        Area = "OPD",
                                        UpdatedBy = x.UpdatedBy,
                                        UpdatedAt = x.UpdatedAt,
                                        CreatedAt = x.CreatedAt,
                                        StatusCode = opd.EDStatus.Code,
                                        StatusEnName = opd.EDStatus.EnName,
                                        StatusViName = opd.EDStatus.ViName,
                                        IsAnesthesia = opd.IsAnesthesia,
                                    }));
                                }
                            }
                            break;
                        case "ED":
                            if (visit.Id.HasValue)
                            {
                                var ed = GetED(visit.Id.Value);
                                var forms = GetFormsED(ed);
                                var dataInForm = forms.FindAll(x => x.Datas != null && x.Datas.Count > 0).ToList();
                                if (dataInForm.Count > 0)
                                {
                                    listBA.AddRange(dataInForm.Select(x => new MedicalRecordViewTMP
                                    {
                                        Id = visit.Id,
                                        AdmittedDate = ed?.AdmittedDate,
                                        VisitCode = ed?.VisitCode,
                                        Specialty = ed.Specialty?.ViName,
                                        EnSpecialty = ed?.Specialty?.EnName,
                                        PrimaryDoctor = ed.PrimaryDoctor?.Username,
                                        ViName = x.ViName,
                                        EnName = x.EnName,
                                        Datas = x.Datas,
                                        Type = x.Type,
                                        Code = x.Code,
                                        Area = "ED",
                                        UpdatedBy = x.UpdatedBy,
                                        UpdatedAt = x.UpdatedAt,
                                        CreatedAt = x.CreatedAt,
                                        StatusCode = ed.EDStatus.Code,
                                        StatusEnName = ed.EDStatus.EnName,
                                        StatusViName = ed.EDStatus.ViName
                                    }));
                                    listBAOrigin.AddRange(dataInForm.Select(x => new MedicalRecordViewTMP
                                    {
                                        Id = visit.Id,
                                        AdmittedDate = ed?.AdmittedDate,
                                        VisitCode = ed?.VisitCode,
                                        Specialty = ed.Specialty?.ViName,
                                        EnSpecialty = ed?.Specialty?.EnName,
                                        PrimaryDoctor = ed.PrimaryDoctor?.Username,
                                        ViName = x.ViName,
                                        EnName = x.EnName,
                                        Datas = x.Datas,
                                        Type = x.Type,
                                        Code = x.Code,
                                        Area = "ED",
                                        UpdatedBy = x.UpdatedBy,
                                        UpdatedAt = x.UpdatedAt,
                                        CreatedAt = x.CreatedAt,
                                        StatusCode = ed.EDStatus.Code,
                                        StatusEnName = ed.EDStatus.EnName,
                                        StatusViName = ed.EDStatus.ViName
                                    }));
                                }
                            }
                            break;
                        case "EOC":
                            if (visit.Id.HasValue)
                            {
                                var eoc = GetEOC(visit.Id.Value);
                                var forms = GetFormsEOC(eoc);
                                var dataInForm = forms.FindAll(x => x.Datas != null && x.Datas.Count > 0).ToList();
                                if (dataInForm.Count > 0)
                                {
                                    listBA.AddRange(dataInForm.Select(x => new MedicalRecordViewTMP
                                    {
                                        Id = visit.Id,
                                        AdmittedDate = eoc?.AdmittedDate,
                                        VisitCode = eoc?.VisitCode,
                                        Specialty = eoc.Specialty?.ViName,
                                        EnSpecialty = eoc?.Specialty?.EnName,
                                        PrimaryDoctor = eoc.PrimaryDoctor?.Username,
                                        ViName = x.ViName,
                                        EnName = x.EnName,
                                        Datas = x.Datas,
                                        Type = x.Type,
                                        Code = x.Code,
                                        Area = "EOC",
                                        UpdatedBy = x.UpdatedBy,
                                        UpdatedAt = x.UpdatedAt,
                                        CreatedAt = x.CreatedAt,
                                        StatusCode = eoc.Status.Code,
                                        StatusEnName = eoc.Status.EnName,
                                        StatusViName = eoc.Status.ViName
                                    }));
                                    listBAOrigin.AddRange(dataInForm.Select(x => new MedicalRecordViewTMP
                                    {
                                        Id = visit.Id,
                                        AdmittedDate = eoc?.AdmittedDate,
                                        VisitCode = eoc?.VisitCode,
                                        Specialty = eoc.Specialty?.ViName,
                                        EnSpecialty = eoc?.Specialty?.EnName,
                                        PrimaryDoctor = eoc.PrimaryDoctor?.Username,
                                        ViName = x.ViName,
                                        EnName = x.EnName,
                                        Datas = x.Datas,
                                        Type = x.Type,
                                        Code = x.Code,
                                        Area = "EOC",
                                        UpdatedBy = x.UpdatedBy,
                                        UpdatedAt = x.UpdatedAt,
                                        CreatedAt = x.CreatedAt,
                                        StatusCode = eoc.Status.Code,
                                        StatusEnName = eoc.Status.EnName,
                                        StatusViName = eoc.Status.ViName
                                    }));
                                }
                            }
                            break;
                    }
                }
            }

            var currentBAs = listBA.GroupBy(x => x.Type).Select(grp => grp.ToList()).ToList();
            List<MedicalRecordView> showBA = new List<MedicalRecordView>();



            /*
            - BA nội khoa: MedicalRecord
            - BA sản: MedicalRecordObstetrics
            - BA phụ: MedicalRecordGynecological
            - BA nhi: MedicalRecordPediatric
            - BA sơ sinh: MedicalRecordNeonatal
            - BA tai mũi họng: A01_039_050919_V
            - BA răng hàm mặt: A01_040_050919_V
            - BA mắt: MedicalRecordEye
            - BA ngoại khoa: TheSurgicalMedicalRecord,TheSurgicalMedicalRecord
            - BA ung bướu: MedicalRecordOncology
            - BA tim mạch: CardiovascularForm
            - Báo cáo y tế: MedicalReport
            */
            var report = new List<string> { "MedicalRecord","MedicalRecordObstetrics","MedicalRecordGynecological","MedicalRecordPediatric","MedicalRecordNeonatal",
                                            "A01_039_050919_V","A01_040_050919_V","MedicalRecordEye","TheSurgicalMedicalRecord","MedicalRecordOncology","CardiovascularForm","MedicalReport"
                                           };

            foreach (var ba in currentBAs)
            {
                if (report.Contains(ba[0].Type))
                {
                    List<MedicalRecordViewTMP> t = new List<MedicalRecordViewTMP>();
                    //if (ba[0].Type == "MedicalReport")
                    //{
                    List<VisitModel> vs = new List<VisitModel>();

                    foreach (var g in group)
                    {
                        if (g.Count == 1)
                        {
                            vs.Add(g[0]);
                        }
                        else
                        {
                            List<VisitModel> tmpvs = new List<VisitModel>();
                            var fist = g[0];
                            for (int i = 1; i < g.Count; i++)
                            {
                                if (fist.Type == "IPD" && g[i].Type == "IPD")
                                {

                                    var isbcyt = ba.FirstOrDefault(x => x.Type == ba[0].Type && x.Id == g[i].Id);
                                    if (isbcyt != null)
                                    {
                                        if (tmpvs.Count > 0)
                                        {
                                            tmpvs.Clear();
                                        }
                                        fist = g[i];
                                        tmpvs.Add(g[i]);
                                    }
                                    var isbcytFist = ba.FirstOrDefault(x => x.Type == ba[0].Type && x.Id == fist.Id);
                                    if (isbcytFist != null)
                                    {
                                        if (tmpvs.Count > 0)
                                        {
                                            tmpvs.Clear();
                                        }
                                        tmpvs.Add(fist);
                                        fist = g[i];
                                    }

                                }
                                else
                                {
                                    if (i == (g.Count - 1))
                                    {
                                        vs.Add(fist);
                                        vs.Add(g[i]);
                                        fist = g[i];
                                    }
                                    else
                                    {
                                        vs.Add(fist);
                                        fist = g[i];
                                    }

                                }
                            }
                            if (tmpvs.Count > 0)
                            {
                                vs.AddRange(tmpvs);
                            }
                        }

                    }
                    foreach (var v in vs)
                    {
                        Guid currentVisit = Guid.Parse(v.Id.ToString());
                        var crba = ba.FirstOrDefault(x => x.Id == currentVisit);
                        if (crba != null)
                        {
                            t.Add(crba);
                        }

                    }

                    if (t.Count > 0)
                    {
                        var show = t.OrderBy(x => x.AdmittedDate).ToList();
                        var m = new MedicalRecordView();
                        m.ViName = show[0].ViName;
                        m.Datas.AddRange(show);

                        string type_form = show[0].Type; //dannv6
                        m.Type = type_form;

                        showBA.Add(m);
                    }

                    //}
                    //else
                    //{
                    //    foreach (var g in group)
                    //    {
                    //        foreach (var b in g)
                    //        {

                    //            var crba = ba.FirstOrDefault(x => x.Id == b.Id);
                    //            if (crba != null)
                    //            {
                    //                t.Add(crba);
                    //            }

                    //        }
                    //    }

                    //    if (t.Count > 0)
                    //    {
                    //        var show = t.OrderBy(x => x.AdmittedDate).ToList();
                    //        var m = new MedicalRecordView();
                    //        m.ViName = show[0].ViName;
                    //        m.Datas.AddRange(show);
                    //        showBA.Add(m);
                    //    }
                    //}
                }
                else
                {
                    var filter = listBAOrigin.FindAll(x => x.Type == ba[0].Type).ToList();
                    if (filter.Count > 0)
                    {
                        var o = filter.OrderBy(x => x.AdmittedDate).ToList();
                        var m = new MedicalRecordView();
                        m.ViName = o[0].ViName;
                        m.Datas.AddRange(o);

                        string type_form = o[0].Type;// dannv6
                        m.Type = type_form;

                        showBA.Add(m);
                    }
                }

            };

            return Content(HttpStatusCode.OK, showBA);
        }
        [HttpGet]
        [Route("api/InfoMedicalRecord/{pid}")]
        [Permission(Code = "MMERE2")]
        public async System.Threading.Tasks.Task<IHttpActionResult> InfoMedicalRecordAPIAsync(string pid, [FromUri] CustomerDetailParameterModel request)
        {
            var customer = ExecQuery.getCustomerByPId(pid);
            if (customer == null)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            var all_status = unitOfWork.EDStatusRepository.Find(e => !e.IsDeleted).ToList();
            var no_examination = all_status.FirstOrDefault(e => e.Code == "OPDNE");
            var results = ExecQuery.getMedicalRecordByCustomerId(customer.Id);
            List<string> list_visit_code = results.Select(x => x.EHOSVisitCode).ToList();

            if (!string.IsNullOrWhiteSpace(customer.PIDEhos) && hasAction("OHMedicalRecord"))
            {
                var ehos = EHosClient.GetVisitHistory(customer.PIDEhos, DateTime.Now);
                if(ehos != null && ehos.Count() > 0)
                {
                    var dataEhos = ehos.Where(x => !list_visit_code.Contains(x.EHOSVisitCode)).Select(x => new InfoRecord
                    {
                        Fullname = x.Fullname,
                        ExaminationTime = x.ExaminationTime,
                        Assessment = x.Assessment,
                        ViName = x.ViName,
                        EnName = x.EnName,
                        PastMedicalHistory = x.PastMedicalHistory,
                        FamilyMedicalHistory = x.FamilyMedicalHistory,
                        HistoryOfAllergies = x.HistoryOfAllergies,
                        HistoryOfPresentIllness = x.HistoryOfPresentIllness,
                        Tests = x.Tests,
                        Diagnosis = x.Diagnosis,
                        ClinicalSymptoms = x.ClinicalSymptoms,
                        ICD = x.ICD,
                        ICDName = x.ICDName,
                        Username = x.Username,
                        EHOSVisitCode = x.EHOSVisitCode,
                        Type = x.Type,
                        ChiefComplain = x.ChiefComplain,
                        InitialDiagnosis = x.InitialDiagnosis,
                        TreatmentPlans = x.TreatmentPlans,
                        DoctorRecommendations = x.DoctorRecommendations,
                        ICDOption = x.ICDOption
                    });
                    results.AddRange(dataEhos);
                }
                
            }

            // VIHC
            if (hasAction("VIHCMedicalRecord")) { 
                var vihc_data = await OHAPIService.GetViHCByPIDAsync(pid);
                var vihc_data_formated = vihc_data.Select(e => new InfoRecord()
                {
                    Id = null,
                    ExaminationTime = (DateTime)e.ExaminationDate,
                    DischargeDate = (DateTime)e.GPCompletedTime,
                    VisitCode = e.VisitCode,
                    StatusCode = e.Status,
                    StatusViName = e.StatusText,
                    StatusEnName = e.StatusText,
                    Type = "VIHC",
                    SpecialtyEnName = "Khám sức khỏe tổng quát",
                    SpecialtySiteEnName = "Khám sức khỏe tổng quát",
                    SiteCode = e.SiteCode,
                    Fullname = e.FullName,
                    DoctorUsername = e.ConclusionDoctor
                }).ToList();
                results.AddRange(vihc_data_formated);
            }
            if (request.Status != null)
                results = results.Where(e => request.ConvertedStatus.Contains(e.StatusId)).ToList();

            if (no_examination != null)
                results = results.Where(e => e.StatusId != no_examination.Id).ToList();

            if (request.VisitCode != null)
                results = results.Where(e => request.ConvertedVisitCode.Contains(e.VisitCode)).ToList();

            if (request.RecordCode != null)
                results = results.Where(e => request.ConvertedRecordCode.Contains(e.RecordCode)).ToList();

            if (request.VisitTypeGroupCode != null)
                results = results.Where(e => request.ConvertedVisitTypeGroupCode.Contains(e.Type)).ToList();

            if (request.Specialty != null)
                results = results.Where(e => request.ConvertedSpecialty.Contains(e.SpecialtyId)).ToList();

            if (request.StartExaminationTime != null && request.EndExaminationTime != null)
                results = results.Where(
                    e => e.ExaminationTime != null &&
                    e.ExaminationTime >= request.ConvertedStartExaminationTime &&
                    e.ExaminationTime <= request.ConvertedEndExaminationTime
                ).ToList();
            else if (request.StartExaminationTime != null)
                results = results.Where(
                    e => e.ExaminationTime != null &&
                    e.ExaminationTime >= request.ConvertedStartExaminationTime
                ).ToList();
            else if (request.EndExaminationTime != null)
                results = results.Where(
                    e => e.ExaminationTime != null &&
                    e.ExaminationTime <= request.ConvertedEndExaminationTime
                ).ToList();

            results = results.OrderByDescending(e => e.ExaminationTime).ToList();
            return Content(HttpStatusCode.OK, results);
        }
    }

    public class MedicalRecordView
    {
        public string Type { get; set; } //dannv6
        public string ViName { get; set; }
        public List<MedicalRecordViewTMP> Datas { get; set; } = new List<MedicalRecordViewTMP>();
    }
    public class MedicalRecordViewTMP
    {
        public Guid? Id { get; set; }
        public DateTime? AdmittedDate { get; set; }
        public string VisitCode { get; set; }
        public string Specialty { get; set; }
        public string EnSpecialty { get; set; }
        public string PrimaryDoctor { get; set; }
        public string ViName { get; set; }
        public string EnName { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public List<MedicalRecordDataViewModel> Datas { get; set; }
        public string Area { get; set; }
        public string StatusCode { get; set; }
        public string StatusViName { get; set; }
        public string StatusEnName { get; set; }
        public bool IsAnesthesia { get; set; } = false;        
    }

    public class RadiologyParamester : IPDThrombosisRiskFactorAssessmentParams
    {
    }

}