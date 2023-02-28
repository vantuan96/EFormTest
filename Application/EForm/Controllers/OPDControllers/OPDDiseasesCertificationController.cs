using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.Controllers.BaseControllers;
using System;
using System.Web.Http;
using System.Net;
using EForm.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using DataAccess.Models;
using Newtonsoft.Json.Linq;
using EForm.Client;
using EForm.Utils;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDDiseasesCertificationController : BaseOPDApiController
    {
        private const string code_doctorConfirm = "DOCTORCONFIRM";
        private const string code_hospitalConfirm = "HOSPITALCONFIRM";
        private const string _is_ExistDiseasesCertification = "OPDOENXNBTYES";

        [HttpGet]
        [Route("api/OPD/DiseasesCertification/{visitId}")]
        [Permission(Code = "OPDXEMGXNBT")]
        public IHttpActionResult GetDiseasesCertification(Guid visitId)
        {
            OPD visit = GetOPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var oen = visit?.OPDOutpatientExaminationNote;
            if (oen == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_OEN_NOT_FOUND);
            var oen_datas = oen.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted)?.ToList();

            var yes = oen_datas?.FirstOrDefault(e => e.Code == _is_ExistDiseasesCertification);
            if (!(yes != null && !string.IsNullOrEmpty(yes?.Value) && Convert.ToBoolean(yes?.Value) == true))
            {
                return Content(HttpStatusCode.NotFound, Message.OPD_OEN_NOT_FOUND);
            }
            var hospital = visit.Site;
            var custumer = visit.Customer;
            var specialty = visit.Specialty;
            string userName_hospitalConfirmation = oen_datas?.FirstOrDefault(e => e.Code == code_hospitalConfirm)?.Value;
            User user_hospitalConfirmation = GetUserConfirm(userName_hospitalConfirmation);
            string userName_doctorConfirmed = oen_datas?.FirstOrDefault(e => e.Code == code_doctorConfirm)?.Value;
            User user_doctorConfirmed = GetUserConfirm(userName_doctorConfirmed);
            string fatherCustormer = "";
            string motherCustormer = "";
            if(custumer?.PID != null)
            {
                var relationships = OHClient.GetRelationshipOfCustormerByPid(custumer.PID);
                fatherCustormer = ChoiceRelationShipResultFromOH(relationships, "FA");
                motherCustormer = ChoiceRelationShipResultFromOH(relationships, "MO");
            }
            var translation_util = new TranslationUtil(unitOfWork, visit.Id, "OPD", "ILLNESS CERTIFICATE");
            var translations = translation_util.GetList();
            return Content(HttpStatusCode.OK, new
            {
                Hospital = new { ViName = hospital?.ViName, EnName = hospital?.EnName },
                NameCustumer = custumer?.Fullname,
                GenderCustumer = custumer?.Gender,
                DateOfBirthCustumer = custumer?.DateOfBirth?.ToString("dd/MM/yyyy"),
                FatherCustumer = fatherCustormer,
                MotherCustumer = motherCustormer,
                Address = custumer?.Address,
                PID = custumer?.PID,
                ExaminationDate = oen?.ExaminationTime?.ToString("dd/MM/yyyy"),
                Diagnose = GetDiagnosisOPD(oen_datas, visit.Version, "MEDICAL CERTIFICATE"),
                TreatmentDirection = oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTP0ANS") == null ? "" : oen_datas?.FirstOrDefault(e => e.Code == "OPDOENTP0ANS").Value,
                Specialty = new { ViName = specialty?.ViName, EnName = specialty?.EnName },
                UserHospitalConfirmation = new { UserName = user_hospitalConfirmation?.Username, FullName = user_hospitalConfirmation?.Fullname },
                UserDoctorConfirmed = new { UserName = user_doctorConfirmed?.Username, FullName = user_doctorConfirmed?.Fullname },
                Location = hospital?.Location == null ? ".........." : hospital.Location,
                Translations = translations
            });
        }

        [HttpPost]
        [Route("api/OPD/DoctorConfirmDiseasesCertification/{visitId}")]
        [Permission(Code = "OPDBSXNGXNBT")]
        public IHttpActionResult DoctorConfirmDiseasesCertification(Guid visitId, [FromBody] JObject req)
        {
            OPD visit = GetOPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.OK, Message.OPD_NOT_FOUND);

            var outPatientExam = visit.OPDOutpatientExaminationNote;
            if (outPatientExam == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_OEN_NOT_FOUND);

            var username = req["username"]?.ToString();
            var password = req["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            string action_user = GetActionOfUser(user, "OPDBSXNGXNBT");
            if (action_user == null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            CreateOfUpdateConfirm(outPatientExam, user, code_doctorConfirm);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [Route("api/OPD/HospitalConfirmDiseasesCertification/{visitId}")]
        [Permission(Code = "OPDBVXNGXNBT")]
        public IHttpActionResult HospitalConfirmDiseasesCertification(Guid visitId, [FromBody] JObject req)
        {
            OPD visit = GetOPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.OK, Message.OPD_NOT_FOUND);

            var outPatientExam = visit.OPDOutpatientExaminationNote;
            if (outPatientExam == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_OEN_NOT_FOUND);

            var username = req["username"]?.ToString();
            var password = req["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            string action_user = GetActionOfUser(user, "OPDBVXNGXNBT");
            if (action_user == null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            CreateOfUpdateConfirm(outPatientExam, user, code_hospitalConfirm);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private bool IsExistDiseasesCertification(List<MappingDatasQuery> datas, string code)
        {
            var data = datas.FirstOrDefault(e => e.Code == code);
            if (data == null)
                return false;

            if (data.Value.ToUpper() == "TRUE")
                return true;

            return false;
        }

        private void CreateOfUpdateConfirm(OPDOutpatientExaminationNote outPatientExam, User user, string code_confirm)
        {
            var dataConfirm = outPatientExam.OPDOutpatientExaminationNoteDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == code_confirm);

            if (dataConfirm == null)
            {
                var newUser_confirm = new OPDOutpatientExaminationNoteData()
                {
                    OPDOutpatientExaminationNoteId = outPatientExam.Id,
                    Code = code_confirm,
                    Value = user.Username,
                };

                unitOfWork.OPDOutpatientExaminationNoteDataRepository.Add(newUser_confirm);
            }
            else
            {
                dataConfirm.Value = user.Username;
                unitOfWork.OPDOutpatientExaminationNoteDataRepository.Update(dataConfirm);
            }

            unitOfWork.Commit();
        }

        private string FormatDiagnose(List<MappingDatasQuery> datas)
        {
            var code_data = datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS");
            string textDiagnose = code_data == null ? "" : code_data.Value == null ? "" : code_data.Value;

            var icdchinh = datas.FirstOrDefault(e => e.Code == "OPDOENICDANS");
            string jsonText = icdchinh == null ? "" : icdchinh.Value;
            if (jsonText == null || jsonText == $"\"\"")
                jsonText = "";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<Dictionary<string, string>> objs = jss.Deserialize<List<Dictionary<string, string>>>(jsonText);
            string _str = String.Empty;
            if (objs != null)
            {
                int lengthOfobjs = objs.Count;
                for (int i = 0; i < lengthOfobjs; i++)
                {
                    var codeIcd10 = objs[i]["code"]?.ToString();
                    if (i == 0)
                        _str += codeIcd10;
                    else
                        _str += $", {codeIcd10}";
                }
            }

            var icdphu = datas.FirstOrDefault(e => e.Code == "OPDOENICDOPT");
            string jsonphu = icdphu == null ? "" : icdphu.Value;
            if (jsonphu == null || jsonphu == $"\"\"")
                jsonphu = "";

            List<Dictionary<string, string>> objs1 = jss.Deserialize<List<Dictionary<string, string>>>(jsonphu);

            if (objs1 != null)
            {
                int lengthOfobjs = objs1.Count;
                for (int j = 0; j < lengthOfobjs; j++)
                {
                    var codeIcd10phu = objs1[j]["code"]?.ToString();
                    if (j == 0)
                    {
                        if (string.IsNullOrEmpty(_str))
                        {
                            _str += codeIcd10phu;
                        }
                        else
                            _str += $", {codeIcd10phu}";
                    }
                    else
                        _str += $", {codeIcd10phu}";
                }
            }

            if (!string.IsNullOrEmpty(_str))
                textDiagnose += $" ({_str})";

            return textDiagnose;
        }


        private List<MappingDatasQuery> GetDatasFromOutpatientExaminationNoteData(OPDOutpatientExaminationNote form)
        {
            var datas = form.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).Select(e => new MappingDatasQuery()
            {
                Code = e.Code,
                Value = e.Value
            }).ToList();

            return datas;
        }

        private User GetUserConfirm(string userName)
        {
            User user = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Username.ToUpper() == userName.ToUpper());

            return user;
        }

        private string ChoiceRelationShipResultFromOH(List<dynamic> relationships, string relationshipCode)
        {
            if (relationships.Count == 0 || relationships == null)
                return "";

            string relationship = (from r in relationships
                                   where r.RelationshipCode == relationshipCode
                                   orderby r.LastUpdated descending
                                   select r.ContactName).FirstOrDefault();
            if (relationship == null)
                return "";

            return relationship;
        }        

        class MappingDatasQuery
        {
            public string Code { get; set; }
            public string Value { get; set; }
        }
    }

}
