using DataAccess.Models;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Models;
using EForm.Models.IPDModels;
using EForm.Models.MedicationAdministrationRecordModels;
using EForm.Utils;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDMedicalRecordExtenstionController : IPDMedicalRecordController
    {
        #region Extentions
        [HttpGet]
        [Route("api/IPD/MedicalRecordExtenstion/Info/{Type}/{Name}/{VisitId}")]
        [Permission(Code = "DGNCTTTGET")]
        public IHttpActionResult GetInfo(string Type,string Name, Guid VisitId)
        {
            var ipd = GetIPD(VisitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var etrMasterDataCode = new string[] { "IPDIAAUHEIGANS", "IPDIAAUWEIGANS" };
            
            Guid gTmp = Guid.Empty;
            if (ipd.IPDInitialAssessmentForAdultId == null)
            {
                var results = GetAllInfoCustomerInAreIPD(ipd);
                if (results.Count > 1)
                {
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i].Id == ipd.Id)
                            continue;

                        gTmp = (Guid)results[i].Id;
                        break;
                    }

                }
            }
            else
            {
                gTmp = (Guid)ipd.IPDInitialAssessmentForAdultId;
            }

            var dataEtr = unitOfWork.IPDInitialAssessmentForAdultDataRepository
               .Find(e => etrMasterDataCode.Contains(e.Code) && e.IPDInitialAssessmentForAdultId == gTmp)
               .Select(e => new MasterDataValue
               {
                   Code = e.Code,
                   Value = e.Value,
               }).ToList();
            return Content(HttpStatusCode.OK, new
            {
                Weight = getValueFromMasterDatas("IPDIAAUWEIGANS", dataEtr),
                Height = getValueFromMasterDatas("IPDIAAUHEIGANS", dataEtr)
            });


        }
        [HttpGet]
        [Route("api/IPD/MedicalRecordExtenstion/{Type}/{Name}/{VisitId}")]
        [Permission(Code = "DGNCTTTGET")]
        public IHttpActionResult GetMedicalRecordExtenstionByVisitId(string Type, string Name, Guid VisitId)
        {
            var ipd = GetIPD(VisitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var form = GetForm(VisitId,Name);              
            bool needCreateRecordExtenstion = false;
            var medical_record = ipd.IPDMedicalRecord;
            if (medical_record != null)
            {
                var part_2 = medical_record.IPDMedicalRecordPart2;
                if (part_2 != null)
                {
                    var part_2_datas = part_2.IPDMedicalRecordPart2Datas;
                    if (part_2_datas != null)
                    {
                        try
                        {
                            needCreateRecordExtenstion = Convert.ToBoolean(part_2_datas.FirstOrDefault(e => e.Code == "IPDMRPT1681").Value);
                        }
                        catch
                        {
                            needCreateRecordExtenstion = false;
                        }
                    }
                }
            }

            if (IPDIsBlock(ipd, Type))
            {
                needCreateRecordExtenstion = false;
            }

            if (form == null)
                return Content(HttpStatusCode.OK, new
                {
                    NotFoundData = NotFoundData(ipd, Type),
                    NeedCreateRecordExtenstion = needCreateRecordExtenstion
                });
            string formCode = Type + "_" + Name;            
            return Content(HttpStatusCode.OK, FormatOutput(Type, Name, ipd, form));
        }
        [HttpPost]
        [Route("api/IPD/MedicalRecordExtenstion/Create/{Type}/{Name}/{VisitId}")]
        [Permission(Code = "DGNCTTTPOST")]
        public IHttpActionResult CreateMedicalRecordExtenstion(string Type, string Name, Guid VisitId)
        {
            var ipd = GetIPD(VisitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var isName = IsName(Name, VisitId);
            if(isName)
                return Content(HttpStatusCode.BadRequest, new
                {
                    ViMessage = "Name đã tồn tại trong MedicalRecordExtenstion",
                    EnMessage = "Name already exists in MedicalRecordExtenstion"
                });
            var form = new IPDMedicalRecordExtenstion()
            {
                VisitId = VisitId,
                Name = Name,
                FormCode = Type,
                Version = 2
            };
            unitOfWork.IPDMedicalRecordExtenstionReponsitory.Add(form);
            string formCode = Type + "_" + Name;
            UpdateVisit(ipd, "IPD");
            //HandleUpdateOrCreateFormDatas(VisitId, form.Id, formCode, request["Datas"]);
            var idForm = form.Id;
            //CreateOrUpdateIPDIPDInitialAssessmentToByFormType(ipd, Type, idForm);
            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                form.VisitId,
                form.CreatedBy,
                form.CreatedAt,
                form.Version
            });
        }
        [HttpPost]
        [Route("api/IPD/MedicalRecordExtenstion/Update/{Type}/{Name}/{VisitId}")]
        [Permission(Code = "DGNCTTTPUT")]
        public IHttpActionResult UpdateMedicalRecordExtenstion(string Type, string Name, Guid VisitId, [FromBody] JObject request)
        {
            var ipd = GetIPD(VisitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var form = GetForm(VisitId,Name);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(ipd, Type));
            string formCode = Type + "_" + Name;
            if (form.DoctorConfirmId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);           
            HandleUpdateOrCreateFormDatas(VisitId, form.Id, formCode, request["Datas"]);
            unitOfWork.IPDMedicalRecordExtenstionReponsitory.Update(form);
            UpdateVisit(ipd, "IPD");
            var formId = form.Id;
            //CreateOrUpdateIPDIPDInitialAssessmentToByFormType(ipd, Type, formId);
            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                form.VisitId,
                form.UpdatedAt,
                form.Version
            });
        }

        [HttpPost]
        [Route("api/IPD/MedicalRecordExtenstion/Confirm/{Type}/{Name}/{VisitId}")]
        public IHttpActionResult ConfirmAPI(string Type, string Name, Guid VisitId,[FromBody] JObject request)
        {
            var visit = GetVisit(VisitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form = GetForm(VisitId,Name);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, Type));                      
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            var successConfirm = ConfirmUser(form, user, request["TypeConfirm"].ToString());
            if (successConfirm)
            {
                UpdateVisit(visit, "IPD");
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            }
        }
        private IPDMedicalRecordExtenstion GetForm(Guid visitId,string name)
        {
            return unitOfWork.IPDMedicalRecordExtenstionReponsitory.Find(e => !e.IsDeleted && e.VisitId == visitId && e.Name == name).FirstOrDefault();
        }
        private bool IsName(string name,Guid visitId)
        {
            var obj = unitOfWork.IPDMedicalRecordExtenstionReponsitory.Find(e => !e.IsDeleted && e.Name == name && e.VisitId == visitId).ToList();
            if(obj.Count > 0 )
            {
                return true;
            }
            return false;
        }
        private bool ConfirmUser(IPDMedicalRecordExtenstion ipdMedicalRecordExtenstion, User user, string kind)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());
            if (kind.ToUpper() == "DOCTOR" && positions.Contains("DOCTOR") && ipdMedicalRecordExtenstion.DoctorConfirmId == null)
            {
                ipdMedicalRecordExtenstion.DoctorConfirmAt = DateTime.Now;
                ipdMedicalRecordExtenstion.DoctorConfirmId = user?.Id;
            }
            else
            {
                return false;
            }
            unitOfWork.IPDMedicalRecordExtenstionReponsitory.Update(ipdMedicalRecordExtenstion);
            unitOfWork.Commit();
            return true;
        }
        private dynamic FormatOutput(string formCode, string name, IPD ipd, IPDMedicalRecordExtenstion fprm)
        {
            string formCodeinFormDatas = formCode + "_" + name;
            var DoctorConfirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.DoctorConfirmId);
            var FullNameCreate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.CreatedBy)?.Fullname;
            var FullNameUpdate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.UpdatedBy)?.Fullname;

            var etrMasterDataCode = new string[] { "IPDIAAUHEIGANS", "IPDIAAUWEIGANS" };
            Guid gTmp = Guid.Empty;
            if (ipd.IPDInitialAssessmentForAdultId == null)
            {
                var resultsInfo= GetAllInfoCustomerInAreIPD(ipd);
                if (resultsInfo.Count > 1)
                {
                    for (int i = 0; i < resultsInfo.Count; i++)
                    {
                        if (resultsInfo[i].Id == ipd.Id)
                            continue;

                        gTmp = (Guid)resultsInfo[i].Id;
                        break;
                    }

                }
                else
                {
                    gTmp = (Guid)ipd.Id;
                }
            }
            else
            {
                gTmp = (Guid)ipd.Id;
            }

            IPD visitTmp = GetVisit(gTmp, "IPD");
            var dataEtr = new List<MasterDataValue>();
                
               

            //Dị ứng
            MedicationInfoModel medicationInfo = new MedicationInfoModel();
            if (gTmp != Guid.Empty)
            {
                dataEtr = unitOfWork.IPDInitialAssessmentForAdultDataRepository
                                  .Find(e => etrMasterDataCode.Contains(e.Code) && e.IPDInitialAssessmentForAdultId == visitTmp.IPDInitialAssessmentForAdultId)
                                  .Select(e => new MasterDataValue
                                  {
                                      Code = e.Code,
                                      Value = e.Value,
                                  }).ToList();
                var ia = visitTmp.IPDInitialAssessmentForAdult;
                if (visitTmp.IsAllergy == true)
                {
                    medicationInfo.CustomerMedicationInfo.Allergy = GenerateAllergyString(visitTmp.KindOfAllergy, visitTmp.Allergy);
                }
                else
                {
                    if (visitTmp.IPDInitialAssessmentForAdult != null)
                    {
                        if (ia != null && ia.IPDInitialAssessmentForAdultDatas != null && ia.IPDInitialAssessmentForAdultDatas.Count > 0)
                        {
                            if (ia.IPDInitialAssessmentForAdultDatas.FirstOrDefault(e => e.Code == "IPDIAAUALLENOO")?.Value.ToUpper() == "TRUE")
                            {
                                medicationInfo.CustomerMedicationInfo.Allergy = "Không";
                            }
                            else if (ia.IPDInitialAssessmentForAdultDatas.FirstOrDefault(e => e.Code == "IPDIAAUALLENPA")?.Value.ToUpper() == "TRUE")
                            {
                                medicationInfo.CustomerMedicationInfo.Allergy = "Không xác định";
                            }
                            else
                            {
                                medicationInfo.CustomerMedicationInfo.Allergy = "";
                            }
                        }
                    }
                }
            }    
            
           
            
            List<FormDataValue> oldDatas = new List<FormDataValue>();
            string oldVisitId = null;
            var results = GetAllInfoCustomerInAreIPD(ipd);
            object specialtyOld = null;
            if (results.Count > 1)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].Id == ipd.Id)
                        continue;
                    Guid oldIpdId = (Guid)results[i].Id;
                    var formOld = unitOfWork.IPDMedicalRecordExtenstionReponsitory.FirstOrDefault(x => x.VisitId == oldIpdId && x.Name == name);
                    if (formOld != null)
                    {
                        oldVisitId = formOld?.VisitId.ToString();
                        var id = formOld?.Id;
                        oldDatas = unitOfWork.FormDatasRepository.Find(e =>
                                                                 !e.IsDeleted &&
                                                                 e.VisitId == oldIpdId &&
                                                                 e.FormCode.Contains("PreProcedureRiskAssessmentForCardiacCatheterization")
                                                              ).Select(f => new FormDataValue { Id = f.Id, Code = f.Code, Value = f.Value, FormId = f.FormId, FormCode = f.FormCode }).ToList();

                        specialtyOld = results[i].Specialty;
                        break;
                    }

                }

            }
            var isNew = fprm.CreatedAt == fprm.UpdatedAt ? true : false;
            return new
            {
                ID = fprm.Id,
                VisitId = fprm.VisitId,
                Datas = isNew ? oldDatas :  GetFormData((Guid)fprm.VisitId, fprm.Id, formCodeinFormDatas),
                CreatedBy = fprm.CreatedBy,
                FullNameCreate = FullNameCreate,
                CreatedAt = fprm.CreatedAt,
                UpdateBy = fprm.UpdatedBy,
                FullNameUpdate = FullNameUpdate,
                UpdatedAt = fprm.UpdatedAt,
                IsLocked = IPDIsBlock(ipd, formCode),
                Confirm = new
                {
                    Doctor = new
                    {
                        UserName = DoctorConfirm?.Username,
                        FullName = DoctorConfirm?.Fullname,
                        ConfirmAt = fprm.DoctorConfirmAt
                    }
                },
                Weight = dataEtr.Count > 0 ? getValueFromMasterDatas("IPDIAAUWEIGANS", dataEtr): null,
                Height = dataEtr.Count > 0 ? getValueFromMasterDatas("IPDIAAUHEIGANS", dataEtr) : null,
                MedicationInfo = medicationInfo,
                fprm.Version,
                InfoOld = new {
                    VisitId = oldVisitId,
                    Datas = oldDatas
                }
            };
        }
        private List<GrowthProcess> GetGrowthProcess(string pid, DateTime? cr)
        {
            if (string.IsNullOrWhiteSpace(pid)) return new List<GrowthProcess>();

            List<VisitSetupFormModel> ipds = (from ipd_sql in unitOfWork.IPDRepository.AsQueryable().Where(e => !e.IsDeleted && e.CreatedAt < cr)
                                              join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                                                  on ipd_sql.CustomerId equals cus_sql.Id
                                              join re_of in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                                 on ipd_sql.Id equals re_of.VisitId
                                              join re_us in unitOfWork.UserRepository.AsQueryable()
                                                  on ipd_sql.PrimaryDoctorId equals re_us.Id into nlist
                                              from re_us in nlist.DefaultIfEmpty()
                                              select new VisitSetupFormModel
                                              {
                                                  Id = ipd_sql.Id,
                                                  Code = re_of.FormCode,
                                                  IPDMedicalRecord = ipd_sql.IPDMedicalRecord,
                                                  PID = cus_sql.PID,
                                                  IPDMedicalRecordId = ipd_sql.IPDMedicalRecordId,
                                                  CreatedAt = ipd_sql.CreatedAt,
                                                  VisitCode = ipd_sql.VisitCode,
                                                  Doctor = re_us.Username,
                                                  FormId = re_of.FormId,
                                                  AdmittedDate = ipd_sql.AdmittedDate
                                              }).Where(e => e.Code == "A01_037_050919_V" && e.PID == pid && e.IPDMedicalRecordId != null).OrderBy(e => e.CreatedAt).Distinct().ToList();



            var data = new List<GrowthProcess>();
            var list_md_code = new List<string>() {
                "IPDMRPT07", "IPDMRPT10", "IPDMRPT12", "IPDMRPT14", "IPDMRPT16", "IPDMRPT18", "IPDMRPT19", "IPDMRPT20", "IPDMRPT21", "IPDMRPT22", "IPDMRPT23", "IPDMRPT24", "IPDMRPT26", "IPDMRPT28", "IPDMRPT29", "IPDMRPT30",
                "IPDMRPT32", "IPDMRPT34", "IPDMRPT36", "IPDMRPT38", "IPDMRPT39", "IPDMRPT40",  "IPDMRPT41", "IPDMRPT42", "IPDMRPT43",
                "IPDMRPT44", "IPDMRPT45", "IPDMRPT46", "IPDMRPT47", "IPDMRPT48", "IPDMRPT49", "IPDMRPT50", "IPDMRPT51", "IPDMRPT52", "IPDMRPT53", "IPDMRPT54", "IPDMRPT55"
            };
            foreach (var ipd in ipds)
            {
                // check part2 của Nhi có tồn tại
                IPDMedicalRecordPart2 part_2 = ipd.IPDMedicalRecord.IPDMedicalRecordPart2;
                if (part_2 != null && ipd.FormId == part_2.Id)
                {
                    var tranferInfo = GetTranferInfo(ipd.Id);
                    var data_part2 = (from ipd_md_sql in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable().Where(e => !e.IsDeleted && e.IPDMedicalRecordPart2Id == part_2.Id && list_md_code.Contains(e.Code))
                                      join re_of in unitOfWork.MasterDataRepository.AsQueryable()
                                      on ipd_md_sql.Code equals re_of.Code
                                      select new MasterDataValue
                                      {
                                          Code = ipd_md_sql.Code,
                                          Value = ipd_md_sql.Value,
                                          UpdatedBy = ipd_md_sql.UpdatedBy,
                                          CreatedBy = ipd_md_sql.CreatedBy,
                                          Label = re_of.ViName,
                                          ViName = re_of.ViName,
                                          EnName = re_of.EnName,
                                          GroupCode = re_of.Group,
                                          Lv = re_of.Level
                                      }
                                      ).OrderBy(e => e.Code).ToList();
                    data.Add(new GrowthProcess
                    {
                        Id = ipd.Id,
                        VisitCode = ipd.VisitCode,
                        RawVisitDate = ipd.AdmittedDate,
                        VisitDate = ipd.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                        Username = ipd.Doctor,
                        DoctorUsername = ipd.Doctor,
                        Data = data_part2
                    });
                    data = data.ToList().OrderByDescending(e => e.RawVisitDate).ToList();
                }
            }
            return data;
        }

        private string GenerateAllergyString(string kindOfAllergy, string allergy)
        {
            string diUng = "";
            if (kindOfAllergy.Contains(','))
            {
                string[] allergyTypes = kindOfAllergy.Split(',');
                for (int i = 0; i < allergyTypes.Length; i++)
                {
                    switch (allergyTypes[i])
                    {
                        case "1":
                            diUng += "Thực phẩm, ";
                            break;
                        case "2":
                            diUng += "Thời tiết, ";
                            break;
                        case "3":
                            diUng += "Thuốc, ";
                            break;
                        case "4":
                            diUng += "Khác, ";
                            break;
                    }
                }

                diUng = diUng.Remove(diUng.LastIndexOf(','), 1);
            }
            else
            {
                switch (kindOfAllergy)
                {
                    case "1":
                        diUng = "Thực phẩm";
                        break;
                    case "2":
                        diUng = "Thời tiết";
                        break;
                    case "3":
                        diUng = "Thuốc";
                        break;
                    case "4":
                        diUng = "Khác";
                        break;
                }
            }

            diUng += $" - {allergy}";
            return diUng;
        }
        #endregion
    }
}
