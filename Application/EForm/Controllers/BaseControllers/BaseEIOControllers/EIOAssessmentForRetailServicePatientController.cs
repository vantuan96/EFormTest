using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.OPDModel;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOAssessmentForRetailServicePatientController: BaseApiController
    {
        protected void CreateAssessmentForRetailServicePatient(dynamic visit, string visit_type)
        {
            EIOAssessmentForRetailServicePatient afrsp = new EIOAssessmentForRetailServicePatient();
            afrsp.TriageDateTime = visit.AdmittedDate;
            afrsp.Version = 2;
            unitOfWork.EIOAssessmentForRetailServicePatientRepository.Add(afrsp);
            EIOStandingOrderForRetailService sofrs = new EIOStandingOrderForRetailService();
            unitOfWork.EIOStandingOrderForRetailServiceRepository.Add(sofrs);

            if (visit_type == "ED")
                CreateEDAssessmentForRetailServicePatient(visit, afrsp.Id, sofrs.Id);
            else if (visit_type == "OPD")
                CreateOPDAssessmentForRetailServicePatient(visit, afrsp.Id, sofrs.Id);
            unitOfWork.Commit();
        }
        private void CreateEDAssessmentForRetailServicePatient(ED visit, Guid afrsp_id, Guid sofrs_id)
        {
            visit.EDAssessmentForRetailServicePatientId = afrsp_id;
            visit.IsRetailService = true;
            visit.EDStandingOrderForRetailServiceId = sofrs_id;
            visit.ATSScale = "6ETRATSSSP";
            unitOfWork.EDRepository.Update(visit);
        }
        private void CreateOPDAssessmentForRetailServicePatient(OPD visit, Guid afrsp_id, Guid sofrs_id)
        {
            visit.EIOAssessmentForRetailServicePatientId = afrsp_id;
            visit.IsRetailService = true;
            visit.EIOStandingOrderForRetailServiceId = sofrs_id;
            unitOfWork.OPDRepository.Update(visit);
        }

        
        protected dynamic GetDetailAssessmentForRetailServicePatient(dynamic visit, EIOAssessmentForRetailServicePatient afrsp,bool isLocked = false)
        {
            return new
            {
                visit.RecordCode,
                afrsp.Id,
                afrsp.Bed,
                afrsp.Version,
                TriageDateTime = afrsp.TriageDateTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Datas = afrsp.EIOAssessmentForRetailServicePatientDatas
                    .Where(i => !i.IsDeleted)
                    .Select(etrd => new { etrd.Id, etrd.Code, etrd.Value })
                    .ToList(),
                IsNew = IsNew(afrsp.CreatedAt, afrsp.UpdatedAt),
                IsLocked = isLocked
            };
        }

        protected dynamic GetLastestRetailVisit24H(string visit_type, Guid? customer_id, Guid ed_id, DateTime? ed_admitted_date)
        {
            if (visit_type == "ED")
                return GetLastestEDRetailVisit24H(customer_id, ed_id, ed_admitted_date);
            else if (visit_type == "OPD")
                return GetLastestOPDRetailVisit24H(customer_id, ed_id, ed_admitted_date);
            return null;
        }
        private ED GetLastestEDRetailVisit24H(Guid? customer_id, Guid ed_id, DateTime? ed_admitted_date)
        {
            var time = DateTime.Now.AddDays(-1);
            return unitOfWork.EDRepository.Find(
                e => !e.IsDeleted &&
                e.Id != ed_id &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.IsRetailService &&
                e.AdmittedDate != null &&
                e.AdmittedDate >= time &&
                e.AdmittedDate < ed_admitted_date
            ).OrderByDescending(e => e.AdmittedDate).FirstOrDefault();
        }
        private OPD GetLastestOPDRetailVisit24H(Guid? customer_id, Guid ed_id, DateTime? ed_admitted_date)
        {
            var time = DateTime.Now.AddDays(-1);
            return unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.Id != ed_id &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.IsRetailService &&
                e.AdmittedDate != null &&
                e.AdmittedDate >= time &&
                e.AdmittedDate < ed_admitted_date
            ).OrderByDescending(e => e.AdmittedDate).FirstOrDefault();
        }


        protected void UpdateVisit(string visit_type, dynamic visit, EIOAssessmentForRetailServicePatient afrs, JObject request)
        {
            if (visit_type == "ED")
                UpdateEDVisit(visit, afrs, request);
            else if (visit_type == "OPD") 
                UpdateOPDVisit(visit, afrs, request);
        }
        private void UpdateEDVisit(ED ed, EIOAssessmentForRetailServicePatient afrs, JObject request)
        {
            DateTime request_triage_datetime = DateTime.ParseExact(request["TriageDateTime"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            if (request_triage_datetime != null && afrs.TriageDateTime != request_triage_datetime)
            {
                afrs.TriageDateTime = request_triage_datetime;

                var etr = ed.EmergencyTriageRecord;
                etr.TriageDateTime = request_triage_datetime;
                unitOfWork.EmergencyTriageRecordRepository.Update(etr, is_anonymous: true);

                ed.AdmittedDate = request_triage_datetime;
                UpdateRecordCodeOfCustomer((Guid)ed.CustomerId);
            }
            var bed = request["Bed"]?.ToString();
            afrs.Bed = bed;
            unitOfWork.EIOAssessmentForRetailServicePatientRepository.Update(afrs);

            ed.Bed = bed;
            ed.Reason = null;
            var user = GetUser();
            if (ed.CurrentNurseId == null)
                ed.CurrentNurseId = user.Id;
            unitOfWork.EDRepository.Update(ed);
            unitOfWork.Commit();
        }
        private void UpdateOPDVisit(OPD opd, EIOAssessmentForRetailServicePatient afrs, JObject request)
        {
            DateTime admited_date = DateTime.ParseExact(request["TriageDateTime"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            if (admited_date != null && afrs.TriageDateTime != admited_date)
            {
                afrs.TriageDateTime = admited_date;

                var iafog = opd.OPDInitialAssessmentForOnGoing;
                iafog.AdmittedDate = admited_date;
                unitOfWork.OPDInitialAssessmentForOnGoingRepository.Update(iafog, is_anonymous: true, is_time_change: false);

                var iafst = opd.OPDInitialAssessmentForShortTerm;
                iafst.AdmittedDate = admited_date;
                unitOfWork.OPDInitialAssessmentForShortTermRepository.Update(iafst, is_anonymous: true, is_time_change: false);

                if (opd.IsTelehealth)
                {
                    var tele = opd.OPDInitialAssessmentForTelehealth;
                    tele.AdmittedDate = admited_date;
                    unitOfWork.OPDInitialAssessmentForTelehealthRepository.Update(tele, is_anonymous: true, is_time_change: false);
                }

                opd.AdmittedDate = admited_date;
                

                UpdateRecordCodeOfCustomer((Guid)opd.CustomerId);
            }
            afrs.Bed = request["Bed"]?.ToString();
            unitOfWork.EIOAssessmentForRetailServicePatientRepository.Update(afrs);

            var user = GetUser();
            if (opd.PrimaryNurseId == null)
                opd.PrimaryNurseId = user.Id;
            unitOfWork.OPDRepository.Update(opd);
            unitOfWork.Commit();
        }


        protected void HandleUpdateOrCreateAssessmentForRetailServicePatientData(dynamic visit, string visit_type, EIOAssessmentForRetailServicePatient afrs, JToken request_etr_data)
        {
            if (visit_type == "ED") { 
                HandleUpdateOrCreateEDAssessmentForRetailServicePatientData(visit, afrs, request_etr_data);
            }
            else if (visit_type == "OPD") { 
                HandleUpdateOrCreateOPDAssessmentForRetailServicePatientData(visit, afrs, request_etr_data);
            }
            //if (visit_type == "OPD" || visit_type == "ED")
            //    HandleUpdateOrCreateTestCovid(visit, request_etr_data, visit_type);
        }

        protected EIOTestCovid2Confirmation HandleUpdateOrCreateTestCovid(dynamic visit, JToken request_etr_data, string visit_type)
        {
            EIOTestCovid2Confirmation data = new EIOTestCovid2Confirmation();
            var isTestCovid2Confirmation = request_etr_data.FirstOrDefault(e => e.Value<string>("Code") == "IEOTESTSARCCOV2ANS" && e.Value<string>("Value") == "True") == null;
            if (visit.EIOTestCovid2ConfirmationId == null) {
                data.IsDeleted = isTestCovid2Confirmation;
                unitOfWork.EIOTestCovid2ConfirmationRepository.Add(data);
                visit.EIOTestCovid2ConfirmationId = data.Id;
                if (visit_type == "ED")
                {
                    unitOfWork.EDRepository.Update(visit);
                }
                else if (visit_type == "OPD")
                {
                    unitOfWork.OPDRepository.Update(visit);
                }
            } else
            {
                data = visit.EIOTestCovid2Confirmation;
                data.IsDeleted = isTestCovid2Confirmation;
                unitOfWork.EIOTestCovid2ConfirmationRepository.Update(data);
            }
            unitOfWork.Commit();
            return data;
        }
        
        private void HandleUpdateOrCreateEDAssessmentForRetailServicePatientData(ED ed, EIOAssessmentForRetailServicePatient afrs, JToken request_etr_data)
        {
            var afrs_datas = afrs.EIOAssessmentForRetailServicePatientDatas.Where(e => !e.IsDeleted).ToList();
            var customer_util = new CustomerUtil(unitOfWork, ed.Customer);
            // var visit_util = new VisitAllergy(ed, "ED");
            var all_dct = new Dictionary<string, string>();
            foreach (var item in request_etr_data)
            {
                var code = item.Value<string>("Code");
                if (code == null) continue;

                var value = item.Value<string>("Value");

                if (Constant.ED_AFRSP_ALLERGIC_CODE.Contains(code))
                    all_dct[Constant.CUSTOMER_ALLERGY_SWITCH[code]] = value;

                var afrs_data = GetOrCreateAssessmentForRetailServicePatientData(afrs_datas, afrs.Id, code);
                if (afrs_data != null)
                    UpdateAssessmentForRetailServicePatientData(customer_util, afrs_data, code, value);
            }

            // customer_util.UpdateAllergy(all_dct);
            // visit_util.UpdateAllergy(allergy_dct);

            if (all_dct.Count > 0)
            {
                if (all_dct["YES"].Trim().ToLower() == "true")
                {
                    ed.IsAllergy = true;
                    ed.KindOfAllergy = all_dct["KOA"];
                    ed.Allergy = all_dct["ANS"];

                    ed.Customer.IsAllergy = true;
                    ed.Customer.KindOfAllergy = all_dct["KOA"];
                    ed.Customer.Allergy = all_dct["ANS"];
                }
                else if (all_dct["NOO"].Trim().ToLower() == "true")
                {
                    ed.IsAllergy = false;
                    ed.KindOfAllergy = "";
                    ed.Allergy = "Không";

                    ed.Customer.IsAllergy = false;
                    ed.Customer.KindOfAllergy = "";
                    ed.Customer.Allergy = "Không";
                }
                else if (all_dct["NPA"].Trim().ToLower() == "true")
                {
                    ed.IsAllergy = null;
                    ed.KindOfAllergy = "";
                    ed.Allergy = "Không xác định";

                    ed.Customer.IsAllergy = false;
                    ed.Customer.KindOfAllergy = "";
                    ed.Customer.Allergy = "Không xác định";
                }
                else
                {
                    ed.IsAllergy = null;
                    ed.KindOfAllergy = "";
                    ed.Allergy = "";
                }

                ed.Customer.LastUpdateAllergy = DateTime.Now;
            }

            afrs.UpdatedBy = GetUser().Username;
            unitOfWork.EIOAssessmentForRetailServicePatientRepository.Update(afrs);
            unitOfWork.Commit();
        }
        private void HandleUpdateOrCreateOPDAssessmentForRetailServicePatientData(OPD opd, EIOAssessmentForRetailServicePatient afrs, JToken request_etr_data)
        {
            var afrs_datas = afrs.EIOAssessmentForRetailServicePatientDatas.Where(e => !e.IsDeleted).ToList();
            var customer_util = new CustomerUtil(unitOfWork, opd.Customer);
            var allergy_dct = new Dictionary<string, string>();
            foreach (var item in request_etr_data)
            {
                var code = item.Value<string>("Code");
                if (code == null) continue;

                var value = item.Value<string>("Value");

                if (Constant.OPD_IAFST_VITAL_SIGN_CODE.Contains(code))
                    UpdateVitalSign(opd, code, value);

                if (Constant.ED_AFRSP_ALLERGIC_CODE.Contains(code))
                    allergy_dct[Constant.CUSTOMER_ALLERGY_SWITCH[code]] = value;

                var afrs_data = GetOrCreateAssessmentForRetailServicePatientData(afrs_datas, afrs.Id, code);
                if (afrs_data != null)
                    UpdateAssessmentForRetailServicePatientData(customer_util, afrs_data, code, value);
            }

            // customer_util.UpdateAllergy(allergy_dct);
            var user = GetUser();
            afrs.UpdatedBy = user.Username;
            unitOfWork.EIOAssessmentForRetailServicePatientRepository.Update(afrs);
            opd.PrimaryNurseId = user.Id;

            var visit_util = new VisitAllergy(opd);
            visit_util.UpdateAllergy(allergy_dct);

            unitOfWork.OPDRepository.Update(opd);
            unitOfWork.Commit();
        }
        private void UpdateVitalSign(OPD opd, string code, string value)
        {
            var oen_data_code = Constant.OPD_OEN_VITAL_SIGN_CODE_SWITCH[code];
            var oen = opd.OPDOutpatientExaminationNote;
            var oen_data = oen.OPDOutpatientExaminationNoteDatas.FirstOrDefault(e => e.Code == oen_data_code);
            if (oen_data == null)
                CreateExaminationNoteData(oen.Id, oen_data_code, value);
            else if (oen_data.Value != value)
                UpdateExaminationNoteData(oen_data, value);
        }
        private void CreateExaminationNoteData(Guid oen_id, string code, string value)
        {
            OPDOutpatientExaminationNoteData new_oen_data = new OPDOutpatientExaminationNoteData
            {
                OPDOutpatientExaminationNoteId = oen_id,
                Code = code,
                Value = value
            };
            unitOfWork.OPDOutpatientExaminationNoteDataRepository.Add(new_oen_data);
        }
        private void UpdateExaminationNoteData(OPDOutpatientExaminationNoteData oen_data, string value)
        {
            oen_data.Value = value;
            unitOfWork.OPDOutpatientExaminationNoteDataRepository.Update(oen_data);
        }
        private EIOAssessmentForRetailServicePatientData GetOrCreateAssessmentForRetailServicePatientData(List<EIOAssessmentForRetailServicePatientData> list_data, Guid afrs_id, string code)
        {
            EIOAssessmentForRetailServicePatientData data = list_data.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null)
                return data;

            data = new EIOAssessmentForRetailServicePatientData
            {
                EDAssessmentForRetailServicePatientId = afrs_id,
                Code = code,
            };
            unitOfWork.EIOAssessmentForRetailServicePatientDataRepository.Add(data);
            return data;
        }
        private void UpdateAssessmentForRetailServicePatientData(CustomerUtil customer_util, EIOAssessmentForRetailServicePatientData afrs_data, string code, string value)
        {
            afrs_data.Value = value;
            unitOfWork.EIOAssessmentForRetailServicePatientDataRepository.Update(afrs_data);

            if (code == "EDAFRSPHEIANS" && !string.IsNullOrEmpty(value))
                customer_util.UpdateHeight(value);
            else if (code == "EDAFRSPWEIANS" && !string.IsNullOrEmpty(value))
                customer_util.UpdateWeight(value);
        }
    }
}