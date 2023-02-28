using DataAccess.Models;
using DataAccess.Models.EIOModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EIOControllers
{
    [SessionAuthorize]
    public class MasterTableController : BaseApiController
    {
        [HttpGet]
        [Route("api/masterdata-form/{visttype}/{formcode}/{VisitID}")]
        [CSRFCheck]
        // [Permission(Code = "EIOMASTERTBL01")]
        public IHttpActionResult GetsAPI(string visttype, string formcode, Guid VisitID)
        {
            var visit = GetVisit(VisitID, visttype);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            //bang t2 thai nghén
            if (formcode == "ThePregnancyCheckupProcess")
            {
                List<MasterdataForms> masterdataForms = new List<MasterdataForms>();
                Guid customerId = visit.CustomerId;
                DateTime admittedDate = (visit as OPD).AdmittedDate;
                List<Guid?> opdIds = unitOfWork.OPDRepository.Find(x => x.CustomerId == customerId && !x.IsDeleted && x.AdmittedDate <= admittedDate).OrderBy(x => x.CreatedAt).Select(x => x?.Id).ToList();
                var eioforms = unitOfWork.EIOFormRepository.Find(x => opdIds.Contains(x.VisitId) && x.FormCode == "ThePregnancyCheckupProcess" && !x.IsDeleted).ToList();
                foreach (var eio in eioforms)
                {

                    var masterform = new List<MasterdataForm>();
                    masterform.Add(FormatOutput(eio, formcode));
                    MasterdataForms masterItem = new MasterdataForms
                    {
                        //VisitId = Guid.Parse(eio.VisitId.ToString()),
                        Forms = masterform,
                        //FormCode = formcode,
                    };
                    masterdataForms.Add(masterItem);
                }
                List<MasterdataForm> forms = new List<MasterdataForm>();
                foreach (var item in masterdataForms)
                {
                    forms.AddRange(item.Forms);
                }
                return Content(HttpStatusCode.OK, new
                {
                    Forms = forms
                });
            }
            else
            {
                if (visttype == "IPD" && formcode == "IPDMATTBL1")
                {
                    var isNew = CheckIsNew(visit);
                    if (isNew)
                    {
                        IPD first_ipd = GetFirstIPD(visit);
                        if (first_ipd != null)
                            VisitID = first_ipd.Id;
                    }
                }

                var forms = GetForms(VisitID, formcode);
                if (forms.Count == 0)
                    return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

                return Content(HttpStatusCode.OK, new MasterdataForms
                {
                    VisitId = VisitID,
                    Forms = forms.Select(form => FormatOutput(form, formcode)).ToList(),
                    FormCode = formcode,
                });
            }

        }
        [HttpPost]
        [Route("api/masterdata-form-with-auth/{visttype}/{formcode}/{VisitID}")]
        [CSRFCheck]
        // [Permission(Code = "EIOMASTERTBL02")]
        public IHttpActionResult HanderUpdateDatasWithAuth(string visttype, string formcode, Guid VisitID, [FromBody] MasterdataForms request)
        {
            foreach (MasterdataForm item in request.Forms)
            {
                if (!item.IsDeleted)
                {
                    var f_id = item.Id;
                    if (item.Id == Guid.Empty || item.Id == null)
                    {
                        var form_data = new EIOForm
                        {
                            VisitId = request.VisitId,
                            FormCode = formcode,
                            FormId = request.FormId,
                            VisitTypeGroupCode = visttype
                        };
                        unitOfWork.EIOFormRepository.Add(form_data);
                        f_id = form_data.Id;
                    }
                    HandleUpdateOrCreateFormDatas((Guid)VisitID, (Guid)f_id, formcode, item.Datas, visttype, true);
                }
                else
                {
                    if (item.Id != Guid.Empty)
                    {
                        var form_to_delete = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.Id == item.Id).FirstOrDefault();
                        unitOfWork.EIOFormRepository.Delete(form_to_delete);
                    }
                }
            }
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.FORM_EXIST);
        }
        [HttpPost]
        [Route("api/masterdata-form/{visttype}/{formcode}/{VisitID}")]
        [CSRFCheck]
        // [Permission(Code = "EIOMASTERTBL02")]
        public IHttpActionResult HanderUpdateDatas(string visttype, string formcode, Guid VisitID, [FromBody] MasterdataForms request)
        {
            var flagDeleteEIOForm = false;
            foreach (MasterdataForm item in request.Forms)
            {
                if (!item.IsDeleted)
                {
                    if(formcode == "IPDSANTHPT1")
                    {
                        if(flagDeleteEIOForm == false)
                        {
                            var form_to_delete = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == VisitID && e.FormCode == formcode).ToList();
                            foreach (var eio in form_to_delete)
                            {
                                unitOfWork.EIOFormRepository.Delete(eio);
                            }
                            unitOfWork.Commit();
                            flagDeleteEIOForm = true;
                        }
                        var form_data = new EIOForm
                        {
                            VisitId = VisitID,
                            FormCode = formcode,
                            FormId = Guid.NewGuid(),
                            VisitTypeGroupCode = visttype
                        };
                        unitOfWork.EIOFormRepository.Add(form_data);
                        HandleUpdateOrCreateFormDatas((Guid)VisitID, (Guid)form_data.Id, formcode, item.Datas, visttype);
                    }
                    else
                    {
                        var f_id = item.Id;
                        if (item.Id == Guid.Empty || item.Id == null)
                        {
                            var formId = item.Datas[0].FormId == null || item.Datas[0].FormId == Guid.Empty ? Guid.NewGuid() : item.Datas[0].FormId;
                            var form_data = new EIOForm
                            {
                                VisitId = VisitID,
                                FormCode = formcode,
                                FormId = formcode == "IPDSANTHPT1" || formcode == "ThePregnancyCheckupProcess" ? formId : request.FormId,

                                VisitTypeGroupCode = visttype
                            };
                            unitOfWork.EIOFormRepository.Add(form_data);
                            HandleUpdateOrCreateFormDatas((Guid)VisitID, (Guid)form_data.Id, formcode, item.Datas, visttype);
                        }
                        else
                        {
                            HandleUpdateOrCreateFormDatas((Guid)VisitID, (Guid)f_id, formcode, item.Datas, visttype);
                        }    
                    }
                    
                }
                else
                {
                     if (item.Id != Guid.Empty)
                     {
                         var form_to_delete = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.Id == item.Id).FirstOrDefault();
                         unitOfWork.EIOFormRepository.Delete(form_to_delete);
                     }
                }
            }
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.FORM_EXIST);
        }
        private void HandleUpdateOrCreateFormDatas(Guid VisitId, Guid FormId, string formCode, List<FormDataValue> request, string visit_type, bool check_auth = false)
        {
            foreach (var item in request)
            {
                var code = item.Code;
                if (string.IsNullOrEmpty(code)) continue;
                var value = item.Value;
                CreateOrUpdateFormDatas(VisitId, FormId, formCode, code, value, visit_type, check_auth);
                if (formCode == "ThePregnancyCheckupProcess")
                {
                    if (item.Items != null)
                    {
                        foreach (var data in item.Items)
                        {
                            var codeData = data.Code;
                            if (string.IsNullOrEmpty(codeData)) continue;
                            var valueData = data.Value;
                            CreateOrUpdateFormDatas(VisitId, FormId, formCode, codeData, valueData, visit_type, check_auth);
                        }
                    }
                }
                
            }
        }
        private EIOForm GetForm(Guid visit_id)
        {
            return unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id).FirstOrDefault();
        }
        private bool CreateOrUpdateFormDatas(Guid visitId, Guid formId, string formCode, string code, string value, string visit_type, bool check_auth = false)
        {
            var finded = unitOfWork.FormDatasRepository.FirstOrDefault(e =>
            !e.IsDeleted &&
            e.VisitId == visitId &&
            e.FormCode == formCode &&
            e.FormId == formId &&
            e.Code == code);
            if (check_auth && finded.CreatedBy != getUsername()) return false;

            if (finded == null)
            {
                if (!string.IsNullOrWhiteSpace(value))
                    unitOfWork.FormDatasRepository.Add(new FormDatas
                    {
                        Code = code,
                        Value = value,
                        FormId = formId,
                        VisitId = visitId,
                        FormCode = formCode,
                        VisitType = visit_type
                    });
            }
            else
            {
                if (finded.Value != value)
                {
                    finded.Value = value;
                    unitOfWork.FormDatasRepository.Update(finded);
                }
            }
            return true;
        }
        private List<EIOForm> GetForms(Guid visit_id, string formcode)
        {
            return unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.FormCode == formcode).OrderBy(e => e.CreatedAt).ToList();
        }
        private List<FormDataValue> GetFormDatas(Guid visitId, Guid formId, string formCode)
        {
            var forms = unitOfWork.FormDatasRepository.Find(e =>
                    !e.IsDeleted &&
                    e.VisitId == visitId &&
                    e.FormCode == formCode &&
                    e.FormId == formId
            ).Select(f => new FormDataValue { Id = f.Id, Code = f.Code, Value = f.Value, FormId = f.FormId, FormCode = f.FormCode }).ToList();
            if(formCode == "ThePregnancyCheckupProcess")
            {
                var masterLevel3 = unitOfWork.MasterDataRepository.Find(e => !e.IsDeleted && e.Form == "A01_067_050919_VE" && e.Level == 3).ToList();
                var tmpForms = new List<FormDataValue>();
                tmpForms.AddRange(forms);
                foreach (var m in masterLevel3)
                {
                    var obj = forms.FirstOrDefault(x => x.Code == m.Code);
                    if (obj != null) forms.Remove(obj);

                } 
                
                for (int i = 0; i < forms.Count; i++)
                {   
                    var codeLevel3s = masterLevel3.AsQueryable().Where(x => x.Group == forms[i].Code).ToList();
                    if(codeLevel3s.Count > 0 && forms[i].Items == null)
                    {
                        forms[i].Items = new List<FormDataValue>();
                    }
                    foreach(var itemLevel3 in codeLevel3s)
                    {
                        var obj = tmpForms.FirstOrDefault(x => x.Code == itemLevel3.Code);
                        if(obj != null)
                        {
                            forms[i].Items.Add(obj);
                        }
                        
                    }    
                    
                }
              
            }
            return forms;
        }
        private MasterdataForm FormatOutput(EIOForm fprm, string formCode)
        {
            return new MasterdataForm
            {
                Id = fprm.Id,
                Datas = GetFormDatas((Guid)fprm.VisitId, fprm.Id, formCode),
                IsDeleted = false,
                VisitId = fprm.VisitId
            };
        }

        private IPD GetFirstIPD(IPD curren_visit)
        {
            if (curren_visit.TransferFromId == null)
                return null;

            var first_visit = unitOfWork.IPDRepository.FirstOrDefault(
                                                         vi => !vi.IsDeleted &&
                                                         vi.CustomerId == curren_visit.CustomerId &&
                                                         vi.HandOverCheckListId == curren_visit.TransferFromId
                                                      );
            if (first_visit == null)
                return null;

            return first_visit;
        }

        private bool CheckIsNew(IPD visit)
        {
            if (visit == null)
                return true;

            var medicalRecord = visit.IPDMedicalRecord;
            if (medicalRecord == null)
                return true;

            var part3 = medicalRecord.IPDMedicalRecordPart3;
            if (part3 == null)
                return true;

            var part3OfPatient = unitOfWork.IPDMedicalRecordOfPatientRepository
                                .FirstOrDefault(
                                  m => !m.IsDeleted &&
                                  m.VisitId == visit.Id &&
                                  m.FormId == part3.Id &&
                                  m.FormCode == "A01_041_050919_V"
                                 );
            if (part3OfPatient == null)
                return true;

            string str_createAt = part3OfPatient.CreatedAt?.ToString("DD/MM/YY HH:mm:ss");
            DateTime createAt = DateTime.ParseExact(str_createAt, "DD/MM/YY HH:mm:ss", CultureInfo.InvariantCulture);
            string str_updateAt = part3OfPatient.UpdatedAt?.ToString("DD/MM/YY HH:mm:ss");
            DateTime updateAt = DateTime.ParseExact(str_updateAt, "DD/MM/YY HH:mm:ss", CultureInfo.InvariantCulture);
            return IsNew(createAt, updateAt);
        }
    }
}