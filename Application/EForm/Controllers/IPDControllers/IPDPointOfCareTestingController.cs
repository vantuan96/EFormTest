using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Models.EDModels;
using EForm.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDPointOfCareTestingController : BaseIPDApiController
    {
        private const string formCode = "A03_038_080322_V";
        private readonly string visit_type = "IPD";
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PointOfCareTesting/ArterialBloodGasTest/Create/{id}")]
        [Permission(Code = "TAOXNTC")]
        public IHttpActionResult CreateIPDArterialBloodGasTestAPI(Guid id)
        {
            IPD visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if (IPDIsBlock(visit, formCode))
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);

            var is_waiting_accept_error_message = GetWaitingNurseAcceptMessage(visit);
            if (is_waiting_accept_error_message != null)
                return Content(HttpStatusCode.OK, is_waiting_accept_error_message);
            if(visit.Version >= 11)
            {
                var new_abgt = CreateIPDArterialBloodGasTestVersion(id, visit.Version);
                var response = BuildArterialBloodGasTestResponseVersion(visit, new_abgt);
                return Content(HttpStatusCode.OK, response);
            }
            else
            {
                var new_abgt = CreateIPDArterialBloodGasTest(id, visit.Version);
                var response = BuildIPDArterialBloodGasTestResponse(visit, new_abgt);
                return Content(HttpStatusCode.OK, response);
            }    
            

            
        }

        [HttpGet]
        [Route("api/IPD/PointOfCareTesting/ArterialBloodGasTest/{id}")]
        [Permission(Code = "XEMXNTC")]
        public IHttpActionResult GetIPDArterialBloodGasTestAPI(Guid id)
        {
            IPD visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var is_waiting_accept_error_message = GetWaitingNurseAcceptMessage(visit);
            if (is_waiting_accept_error_message != null)
                return Content(HttpStatusCode.OK, is_waiting_accept_error_message);

            var forms = GetListFormIPDArterialBloodGasTest(visit);
            var result = new
            {
                forms,
                IsLocked = IPDIsBlock(visit, formCode)
            };

            return Content(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/IPD/PointOfCareTesting/ArterialBloodGasTest/Detail/{id}")]
        [Permission(Code = "XEMXNTC")]
        public IHttpActionResult GetDetailArterialBloodGasTestAPI(Guid id)
        {
            var abgt = unitOfWork.EDArterialBloodGasTestRepository.GetById(id);
            if (abgt == null)
                return Content(HttpStatusCode.NotFound, Message.ED_AGBT_NOT_FOUND);

            IPD visit = GetIPD((Guid)abgt.VisitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            if(visit.Version >= 11)
            {
                var result = BuildArterialBloodGasTestResponseVersion(visit, abgt);
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                var result = BuildIPDArterialBloodGasTestResponse(visit, abgt);
                return Content(HttpStatusCode.OK, result);
            }    
            
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PointOfCareTesting/ArterialBloodGasTest/Update/{id}")]
        [Permission(Code = "SUAXNTC")]
        public IHttpActionResult UpdateIPDArterialBloodGasTestAPI(Guid id, [FromBody] JObject request)
        {
            var abgt = unitOfWork.EDArterialBloodGasTestRepository.GetById(id);
            if (abgt == null)
                return Content(HttpStatusCode.NotFound, Message.ED_AGBT_NOT_FOUND);

            IPD visit = GetIPD((Guid)abgt.VisitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            var ipdblock = IPDIsBlock(visit, formCode, id);
            if (ipdblock)
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            if (abgt.DoctorAcceptId != null)
                return Content(HttpStatusCode.BadRequest, Message.DOCTOR_ACCEPTED);

            UpdateIPDArterialBloodGasTest(abgt, request, visit.Version);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PointOfCareTesting/ArterialBloodGasTest/Accept/{id}")]
        [Permission(Code = "XEMXNTC")]
        public IHttpActionResult AcceptArterialBloodGasTestAPI(Guid id, [FromBody] JObject request)
        {
            var abgt = unitOfWork.EDArterialBloodGasTestRepository.GetById(id);
            if (abgt == null)
                return Content(HttpStatusCode.NotFound, Message.ED_AGBT_NOT_FOUND);

            IPD visit = GetIPD((Guid)abgt.VisitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var lock24h = IPDIsBlock(visit, formCode, id);
            if (lock24h)
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);


            if (visit.Version >= 7 && request["kind"]?.ToString()?.ToUpper() == "CREATED_CONFIRM")
            {
                if (abgt.CreatedBy.ToLower() != user.Username.ToLower())
                    return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
                abgt.ExecutionTime = DateTime.Now;
                abgt.ExecutionUserId = user.Id;
                unitOfWork.EDArterialBloodGasTestRepository.Update(abgt);
                unitOfWork.Commit();
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }

            if (abgt.DoctorAcceptId != null)
                return Content(HttpStatusCode.BadRequest, Message.DOCTOR_ACCEPTED);

            var acction = GetActionOfUser(user, "XACNHANXNTC");
            if (acction == null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            abgt.DoctorAcceptId = user.Id;
            abgt.AcceptTime = DateTime.Now;
            unitOfWork.EDArterialBloodGasTestRepository.Update(abgt);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private EDArterialBloodGasTest CreateIPDArterialBloodGasTest(Guid visit_id, int version)
        {
            Guid? user_id = GetUser().Id;
            EDArterialBloodGasTest new_abgt = new EDArterialBloodGasTest()
            {
                ExecutionUserId = version >= 7 ? null : user_id,
                VisitId = visit_id,
                VisitTypeGroupCode = "IPD"
            };
            unitOfWork.EDArterialBloodGasTestRepository.Add(new_abgt);

            var master_data_id = unitOfWork.EDPointOfCareTestingMasterDataRepository.Find(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Form) &&
                e.Form.Equals("A03_038_080322_V")
            ).Select(e => e.Id).ToList();
            foreach (var id in master_data_id)
            {
                EDArterialBloodGasTestData data = new EDArterialBloodGasTestData()
                {
                    EDArterialBloodGasTestId = new_abgt.Id,
                    EDPointOfCareTestingMasterDataId = id,
                };
                unitOfWork.EDArterialBloodGasTestDataRepository.Add(data);
            }
            unitOfWork.Commit();
            return new_abgt;
        }

        private dynamic BuildIPDArterialBloodGasTestResponse(IPD visit, EDArterialBloodGasTest abgt)
        {
            var customer = visit.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            string exe_time = abgt.ExecutionTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var exe_user = abgt.ExecutionUser;
            UserModel exe_user_detail = new UserModel();
            if (exe_user != null)
                exe_user_detail = GetUserInfo(exe_user);

            string accept_time = abgt.AcceptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var doc_accept = abgt.DoctorAccept;
            UserModel doc_accept_detail = new UserModel();
            if (doc_accept != null)
                doc_accept_detail = GetUserInfo(doc_accept);

            var datas = abgt.EDArterialBloodGasTestDatas.Where(e => !e.IsDeleted)
                .Select(e => new
                {
                    e.Id,
                    e.EDPointOfCareTestingMasterData?.Order,
                    e.EDPointOfCareTestingMasterData?.Name,
                    //e.EDPointOfCareTestingMasterData?.ViAge,
                    //e.EDPointOfCareTestingMasterData?.EnAge,
                    e.EDPointOfCareTestingMasterData?.LowerLimit,
                    e.EDPointOfCareTestingMasterData?.HigherLimit,
                    e.EDPointOfCareTestingMasterData?.LowerAlert,
                    e.EDPointOfCareTestingMasterData?.HigherAlert,
                    e.Result,
                    e.EDPointOfCareTestingMasterData?.Unit,
                }).OrderBy(e => e.Order).ToList();

            var spec = visit.Specialty;
            return new
            {
                abgt.Id,
                customer.Fullname,
                customer.PID,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Gender = gender,
                IsNew = IsNew(abgt.CreatedAt, abgt.UpdatedAt),
                Specialty = new { spec?.ViName, spec?.EnName },
                abgt.UseBreathingMachine,
                abgt.BreathingMode,
                abgt.BP,
                abgt.Vt,
                abgt.F,
                abgt.RR,
                abgt.FIO2,
                abgt.T,
                abgt.Upload,
                ExecutionTime = exe_time,
                ExecutionUser = ForMatUserModel(exe_user_detail),
                AcceptTime = accept_time,
                DoctorAccept = ForMatUserModel(doc_accept_detail),
                Datas = datas,
                abgt.CollectionSite,
                abgt.AllenTest,
                UpdatedBy = abgt.UpdatedBy,
                UpdatedAt = abgt.UpdatedAt,
                IsLocked = IPDIsBlock(visit, formCode, abgt.Id),
                CreatedBy = abgt.CreatedBy,
                CreatedAt = abgt.CreatedAt,
                UpdatedForPrint = abgt.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                visit.Version
            };
        }

        private UserModel ForMatUserModel(UserModel user)
        {
            return new UserModel()
            {
                Username = user?.Username,
                Fullname = user?.Fullname,
                FullShortName = user?.Fullname,
                Department = user?.Department,
                Title = user?.Title,
                Mobile = user?.Mobile
            };
        }

        private void UpdateIPDArterialBloodGasTest(EDArterialBloodGasTest abgt, JObject request, int version)
        {
            var use_bm = request["UseBreathingMachine"]?.ToString();
            if (!string.IsNullOrEmpty(use_bm))
                abgt.UseBreathingMachine = bool.Parse(use_bm);
            abgt.BreathingMode = request["BreathingMode"]?.ToString();
            abgt.BP = request["BP"]?.ToString();
            abgt.Vt = request["Vt"]?.ToString();
            abgt.F = request["F"]?.ToString();
            abgt.RR = request["RR"]?.ToString();
            abgt.FIO2 = request["FIO2"]?.ToString();
            abgt.T = request["T"]?.ToString();
            abgt.Upload = request["Upload"].ToString();
            abgt.ExecutionTime = version >= 7 ? new Nullable<DateTime>() : DateTime.Now;
            abgt.ExecutionUserId = version >= 7 ? null : GetUser()?.Id;
            abgt.AllenTest = request["AllenTest"]?.ToString();
            abgt.CollectionSite = request["CollectionSite"]?.ToString();
            foreach (var item in request["Datas"])
            {
                var id = new Guid(item.Value<string>("Id"));
                var result = item.Value<string>("Result");
                if (result != null)
                {
                    var data = unitOfWork.EDArterialBloodGasTestDataRepository.GetById(id);
                    data.Result = result;
                    unitOfWork.EDArterialBloodGasTestDataRepository.Update(data);
                }
            }
            unitOfWork.EDArterialBloodGasTestRepository.Update(abgt);
            unitOfWork.Commit();
        }

        private List<dynamic> GetListFormIPDArterialBloodGasTest(IPD visit)
        {
            var forms = (from f in unitOfWork.EDArterialBloodGasTestRepository.AsQueryable()
                         where !f.IsDeleted && !string.IsNullOrEmpty(f.VisitTypeGroupCode)
                         && f.VisitTypeGroupCode == "IPD" && f.VisitId == visit.Id
                         orderby f.CreatedAt
                         select new
                         {
                             f.Id,
                             f.CreatedAt,
                             f.CreatedBy,
                             visit.Version
                         }).ToList();

            return new List<dynamic>(forms);
        }

        private string BuildJsonUploadFile(JToken upload_request, string form)
        {
            var upload_path = System.Configuration.ConfigurationManager.AppSettings["FilePath"];
            var upload_list = new List<dynamic>();

            string[] fileExtentions = { "png", "jpeg", "jpg", "gif", "bmp" };
            foreach (var up in upload_request)
            {
                var path = up.ToString();
                if (!path.Contains("/Temp/"))
                {
                    upload_list.Add(path);
                    continue;
                }

                foreach (var exten in fileExtentions)
                {
                    if (path.Contains("/" + exten))
                    {
                        path = path.Replace("/" + exten, "." + exten);
                        break;
                    }
                }

                string destination_file = path.Replace("Temp", form);
                string file_name = destination_file.Split('/').Last();
                string destination_folder = destination_file.Replace(file_name, "");
                string destination_physic_folder = $"{upload_path}/{destination_folder}";

                bool exists = System.IO.Directory.Exists(destination_physic_folder);
                if (!exists)
                    System.IO.Directory.CreateDirectory(destination_physic_folder);

                string physical_soure = $"{upload_path}/{path}";
                string physical_destination = $"{upload_path}/{destination_file}";
                System.IO.File.Move(physical_soure, physical_destination);
                upload_list.Add(destination_file);
            }
            return JsonConvert.SerializeObject(upload_list);
        }

        #region ChemicalBiologyTest
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PointOfCareTesting/ChemicalBiologyTest/Create/{type}/{visitId}/{version}")]
        [Permission(Code = "IPOCT6")]
        public IHttpActionResult CreateChemicalBiologyTestAPI(Guid visitId, string type = "A03_039_080322_V", string version = "2")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var new_cbt = CreateEDChemicalBiologyTest(visit.Id, version, visit.Version);

            var response = BuildChemicalBiologyTestResponse(new_cbt);
            return Content(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [Route("api/IPD/PointOfCareTesting/ChemicalBiologyTest/{type}/{visitId}")]
        [Permission(Code = "IPOCT7")]
        public IHttpActionResult GetChemicalBiologyTestAPI(Guid visitId, string type = "A03_039_080322_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var response = unitOfWork.EDChemicalBiologyTestRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visitId &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode.Equals("IPD")
            ).OrderBy(e => e.CreatedAt).Select(e => new
            {
                e.Id,
                e.CreatedAt,
                e.CreatedBy,
                e.UpdatedAt,
                e.UpdatedBy,
                e.Version
            });
            return Content(HttpStatusCode.OK, new
            {
                Datas = response,
                IsLocked = IPDIsBlock(visit, type),
                Version = visit.Version >= 7 ? visit.Version : 2
            });
        }

        [HttpGet]
        [Route("api/IPD/PointOfCareTesting/ChemicalBiologyTest/{type}/{visitId}/{id}")]
        [Permission(Code = "IPOCT7")]
        public IHttpActionResult GetDetialArterialBloodGasTestAPI(Guid visitId, Guid id, string type = "A03_039_080322_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var cbt = unitOfWork.EDChemicalBiologyTestRepository.GetById(id);
            if (cbt == null)
                return Content(HttpStatusCode.NotFound, Message.ED_CBT_NOT_FOUND);

            var response = BuildChemicalBiologyTestResponse(cbt);
            return Content(HttpStatusCode.OK, new
            {
                response,
                IsLocked = IPDIsBlock(visit, type, cbt.Id)
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PointOfCareTesting/ChemicalBiologyTest/Update/{type}/{visitId}/{id}")]
        [Permission(Code = "IPOCT9")]
        public IHttpActionResult UpdateChemicalBiologyTestAPI(Guid visitId, Guid id, [FromBody] JObject request, string type = "A03_039_080322_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var cbt = unitOfWork.EDChemicalBiologyTestRepository.GetById(id);
            if (cbt == null)
                return Content(HttpStatusCode.NotFound, Message.ED_CBT_NOT_FOUND);

            if (cbt.DoctorAcceptId == null)
                UpdateEDChemicalBiologyTest(ref cbt, request, visit.Version);
            return Content(HttpStatusCode.OK, new { cbt.Id, cbt.UpdatedAt, cbt.UpdatedBy, cbt.Version });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PointOfCareTesting/ChemicalBiologyTest/Accept/{type}/{visitId}/{id}")]
        [Permission(Code = "IPOCT7")]
        public IHttpActionResult AcceptChemicalBiologyTestAPI(Guid visitId, Guid id, [FromBody] JObject request, string type = "A03_039_080322_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var cbt = unitOfWork.EDChemicalBiologyTestRepository.GetById(id);
            if (cbt == null)
                return Content(HttpStatusCode.NotFound, Message.ED_CBT_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            if (visit.Version >= 7 && request["kind"]?.ToString()?.ToUpper() == "CREATED_CONFIRM")
            {
                if (cbt.CreatedBy.ToLower() != user.Username.ToLower())
                    return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
                cbt.ExecutionTime = DateTime.Now;
                cbt.ExecutionUserId = user.Id;
                unitOfWork.EDChemicalBiologyTestRepository.Update(cbt);
                unitOfWork.Commit();
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }

            if (cbt.DoctorAcceptId != null)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            var acction = GetActionOfUser(user, "IPOCT10");
            if (acction == null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            cbt.DoctorAcceptId = user.Id;
            cbt.AcceptTime = DateTime.Now;
            unitOfWork.EDChemicalBiologyTestRepository.Update(cbt);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        private EDChemicalBiologyTest CreateEDChemicalBiologyTest(Guid visit_id, string version, int version_visit)
        {
            Guid? user_id = GetUser().Id;
            int iVersion = Convert.ToInt32(version);
            EDChemicalBiologyTest new_abgt = new EDChemicalBiologyTest()
            {
                ExecutionUserId = version_visit >= 7 ? null : user_id,
                VisitId = visit_id,
                VisitTypeGroupCode = "IPD",
                Version = version_visit >= 7 ? version_visit : iVersion
            };
            unitOfWork.EDChemicalBiologyTestRepository.Add(new_abgt);

            var master_data_id = unitOfWork.EDPointOfCareTestingMasterDataRepository.Find(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Form) &&
                e.Form.Equals("EDChemicalBiologyTest")
                && e.Version == iVersion
            ).Select(e => e.Id).ToList();
            foreach (var id in master_data_id)
            {
                EDChemicalBiologyTestData data = new EDChemicalBiologyTestData()
                {
                    EDChemicalBiologyTestId = new_abgt.Id,
                    EDPointOfCareTestingMasterDataId = id,
                };
                unitOfWork.EDChemicalBiologyTestDataRepository.Add(data);
            }
            unitOfWork.Commit();
            return new_abgt;
        }
        private dynamic BuildChemicalBiologyTestResponse(EDChemicalBiologyTest cbt)
        {
            var ipd = unitOfWork.IPDRepository.GetById((Guid)cbt.VisitId);
            var customer = ipd.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            string exe_time = cbt.ExecutionTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var exe_user = cbt.ExecutionUser;
            UserModel exe_user_detail = new UserModel();
            if (exe_user != null)
                exe_user_detail = GetUserInfo(exe_user);

            string accept_time = cbt.AcceptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var doc_accept = cbt.DoctorAccept;
            UserModel doc_accept_detail = new UserModel();
            if (doc_accept != null)
                doc_accept_detail = GetUserInfo(doc_accept);

            var datas = cbt.EDChemicalBiologyTestDatas.Where(e => !e.IsDeleted)
                .Select(e => new
                {
                    e.Id,
                    e.EDPointOfCareTestingMasterData?.Order,
                    e.EDPointOfCareTestingMasterData?.Name,
                    e.EDPointOfCareTestingMasterData?.ViAge,
                    e.EDPointOfCareTestingMasterData?.EnAge,
                    e.EDPointOfCareTestingMasterData?.LowerLimit,
                    e.EDPointOfCareTestingMasterData?.HigherLimit,
                    e.EDPointOfCareTestingMasterData?.LowerAlert,
                    e.EDPointOfCareTestingMasterData?.HigherAlert,
                    e.Result,
                    e.EDPointOfCareTestingMasterData?.Unit,
                    e.EDPointOfCareTestingMasterData?.CreatedAt,
                }).OrderBy(e => e.CreatedAt).ToList();

            var spec = ipd.Specialty;
            return new
            {
                cbt.Id,
                customer.Fullname,
                customer.PID,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Gender = gender,
                IsNew = IsNew(cbt.CreatedAt, cbt.UpdatedAt),
                Specialty = new { spec?.ViName, spec?.EnName },
                cbt.Upload,
                ExecutionTime = exe_time,
                ExecutionUser = exe_user_detail,
                AcceptTime = accept_time,
                DoctorAccept = doc_accept_detail,
                Datas = datas,
                CreatedBy = cbt.CreatedBy,
                CreatedAt = cbt.CreatedAt,
                UpdateBy = cbt.UpdatedBy,
                UpdateAt = cbt.UpdatedAt,
                Version = cbt.Version,

            };
        }
        private void UpdateEDChemicalBiologyTest(ref EDChemicalBiologyTest cbt, JObject request, int version)
        {
            cbt.Upload = request["Upload"].ToString();
            cbt.ExecutionTime = version >= 7 ? new Nullable<DateTime>() : DateTime.Now;
            cbt.ExecutionUserId = version >= 7 ? null : GetUser()?.Id;
            foreach (var item in request["Datas"])
            {
                var id = new Guid(item.Value<string>("Id"));
                var result = item.Value<string>("Result");
                if (result != null)
                {
                    var data = unitOfWork.EDChemicalBiologyTestDataRepository.GetById(id);
                    data.Result = result;
                    unitOfWork.EDChemicalBiologyTestDataRepository.Update(data);
                }
            }
            unitOfWork.EDChemicalBiologyTestRepository.Update(cbt);
            unitOfWork.Commit();
        }
        private dynamic BuildArterialBloodGasTestResponseVersion(IPD visit, EDArterialBloodGasTest abgt)
        {
            var customer = visit.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            string exe_time = abgt.ExecutionTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var exe_user = abgt.ExecutionUser;
            UserModel exe_user_detail = new UserModel();
            if (exe_user != null)
                exe_user_detail = GetUserInfo(exe_user);

            string accept_time = abgt.AcceptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var doc_accept = abgt.DoctorAccept;
            UserModel doc_accept_detail = new UserModel();
            if (doc_accept != null)
                doc_accept_detail = GetUserInfo(doc_accept);

            var datas = abgt.EDArterialBloodGasTestDatas.Where(e => !e.IsDeleted)
                .Select(e => new
                {
                    e.Id,
                    e.EDPointOfCareTestingMasterData?.Order,
                    e.EDPointOfCareTestingMasterData?.Name,
                    e.EDPointOfCareTestingMasterData?.ViAge,
                    e.EDPointOfCareTestingMasterData?.EnAge,
                    e.EDPointOfCareTestingMasterData?.LowerLimit,
                    e.EDPointOfCareTestingMasterData?.HigherLimit,
                    e.EDPointOfCareTestingMasterData?.LowerAlert,
                    e.EDPointOfCareTestingMasterData?.HigherAlert,
                    e.Result,
                    e.EDPointOfCareTestingMasterData?.Unit,
                    e.EDPointOfCareTestingMasterData?.CreatedAt
                }).OrderBy(e => e.Order).ToList();

            var name = datas.GroupBy(x => x.Name).Select(x => x.FirstOrDefault()).ToList();
            List<InfoCartridgeCG4Model> infos = new List<InfoCartridgeCG4Model>();
            foreach (var item in name)
            {
                List<ForPerson> forPersons = new List<ForPerson>();
                var data = datas.Where(x => x.Name == item.Name).OrderBy(x => x.CreatedAt).ToList();
                foreach (var itemdata in data)
                {
                    InfoCartridgeCG4Model info = new InfoCartridgeCG4Model
                    {
                        Name = item.Name,
                        Result = itemdata.Result,
                        Unit = itemdata.Unit,
                        Id = itemdata.Id,
                        Order = (int)itemdata.Order,
                        ViAge = itemdata.ViAge,
                        EnAge = itemdata.EnAge,
                        LowerLimit = itemdata.LowerLimit,
                        HigherLimit = itemdata.HigherLimit,
                        LowerAlert = itemdata.LowerAlert,
                        HigherAlert = itemdata.HigherAlert
                    };
                    infos.Add(info);
                }
            }

            var spec = visit.Specialty;
            return new
            {
                abgt.Id,
                customer.Fullname,
                customer.PID,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Gender = gender,
                IsNew = IsNew(abgt.CreatedAt, abgt.UpdatedAt),
                Specialty = new { spec?.ViName, spec?.EnName },
                abgt.UseBreathingMachine,
                abgt.BreathingMode,
                abgt.BP,
                abgt.Vt,
                abgt.F,
                abgt.RR,
                abgt.FIO2,
                abgt.T,
                abgt.Upload,
                ExecutionTime = exe_time,
                ExecutionUser = ForMatUserModel(exe_user_detail),
                AcceptTime = accept_time,
                DoctorAccept = ForMatUserModel(doc_accept_detail),
                Datas = infos,
                abgt.CollectionSite,
                abgt.AllenTest,
                UpdatedBy = abgt.UpdatedBy,
                UpdatedAt = abgt.UpdatedAt,
                IsLocked = IPDIsBlock(visit, formCode, abgt.Id),
                CreatedBy = abgt.CreatedBy,
                CreatedAt = abgt.CreatedAt,
                UpdatedForPrint = abgt.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                visit.Version
            };
        }
        private EDArterialBloodGasTest CreateIPDArterialBloodGasTestVersion(Guid visit_id, int version)
        {
            Guid? user_id = GetUser().Id;
            EDArterialBloodGasTest new_abgt = new EDArterialBloodGasTest()
            {
                ExecutionUserId = version >= 7 ? null : user_id,
                VisitId = visit_id,
                VisitTypeGroupCode = "IPD"
            };
            unitOfWork.EDArterialBloodGasTestRepository.Add(new_abgt);
            // switch (version) { }
            // V4 = A03_038_061222_ V_XNTC
            var master_data_id = unitOfWork.EDPointOfCareTestingMasterDataRepository.Find(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Form) &&
                e.Form.Equals("A03_038_061222_V_XNTC")
            ).Select(e => e.Id).ToList();
            foreach (var id in master_data_id)
            {
                EDArterialBloodGasTestData data = new EDArterialBloodGasTestData()
                {
                    EDArterialBloodGasTestId = new_abgt.Id,
                    EDPointOfCareTestingMasterDataId = id,
                };
                unitOfWork.EDArterialBloodGasTestDataRepository.Add(data);
            }
            unitOfWork.Commit();
            return new_abgt;
        }
        #endregion
    }
}
