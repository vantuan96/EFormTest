using DataAccess.Models;
using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOProcedureSummaryController : BaseApiController
    {
        protected EIOProcedureSummary GetProcedureSummary(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOProcedureSummaryRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitTypeGroupCode == visit_type
            );
        }
        protected List<EIOProcedureSummary> GetListProcedureSummary(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOProcedureSummaryRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitTypeGroupCode == visit_type
            ).OrderBy(e => e.CreatedAt).ToList();
        }
        protected EIOProcedureSummary GetProcedureSummary(Guid id)
        {
            return unitOfWork.EIOProcedureSummaryRepository.GetById(id);
        }
        protected EIOProcedureSummary CreateProcedureSummary(Guid visit_id, string visit_type)
        {
            var procedure = new EIOProcedureSummary()
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type,
                Version = "2"
            };
            unitOfWork.EIOProcedureSummaryRepository.Add(procedure);
            CreateSurgeryCertificateByProcedureSummary(procedure);
            unitOfWork.Commit();
            return procedure;
        }

        protected void HandleUpdateProcedureSummary(EIOProcedureSummary procedure, JToken request)
        {
            var procedure_datas = procedure.EIOProcedureSummaryDatas.Where(i => !i.IsDeleted).ToList();
            foreach (var item in request)
            {
                var code = item.Value<string>("Code");
                if (code == null)
                    continue;

                var value = item.Value<string>("Value");
                var data = GetOrCreateProcedureSummaryData(procedure_datas, procedure.Id, code);
                if (data != null)
                    UpdateProcedureSummaryData(data, code, value);
            }
            var user = GetUser();
            procedure.UpdatedBy = user.Username;
            //procedure.UpdatedAt = DateTime.Now;
            unitOfWork.EIOProcedureSummaryRepository.Update(procedure);
            UpdateSurgeryCertificateByProcedureSummary(procedure);
            unitOfWork.Commit();
        }

        private void UpdateSurgeryCertificateByProcedureSummary(EIOProcedureSummary procedure)
        {
            var form = unitOfWork.IPDSurgeryCertificateRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == procedure.VisitId && e.FormId == procedure.Id);
            if (form == null)
                return;

            unitOfWork.IPDSurgeryCertificateRepository.Update(form);
        }

        private void CreateSurgeryCertificateByProcedureSummary(EIOProcedureSummary procedure)
        {
            // Không tạo ra giấy chứng nhận phẫu thuật khi Phiếu phẫu thuật thủ thuật không phải khu IPD 
            if (procedure.VisitTypeGroupCode != "IPD")
                return;

            var checkNewForm = (from f in unitOfWork.IPDSurgeryCertificateRepository.AsQueryable()
                                where !f.IsDeleted && f.VisitId == procedure.VisitId
                                && f.VisitTypeGroupCode == procedure.VisitTypeGroupCode
                                && f.FormId == null
                                select f).FirstOrDefault();

            if (checkNewForm != null)
                return;

            var surgery = new IPDSurgeryCertificate()
            {
                VisitId = procedure.VisitId,
                VisitTypeGroupCode = procedure.VisitTypeGroupCode,
                FormId = procedure.Id
            };
            unitOfWork.IPDSurgeryCertificateRepository.Add(surgery);
        }

        private EIOProcedureSummaryData GetOrCreateProcedureSummaryData(List<EIOProcedureSummaryData> list_data, Guid ps_id, string code)
        {
            EIOProcedureSummaryData data = list_data.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null)
                return data;

            data = new EIOProcedureSummaryData
            {
                EIOProcedureSummaryId = ps_id,
                Code = code,
            };
            unitOfWork.EIOProcedureSummaryDataRepository.Add(data);
            return data;
        }
        private void UpdateProcedureSummaryData(EIOProcedureSummaryData data, string code, string value)
        {
            if ("OPDTTTTTGLANS".Contains(code) && !Validator.ValidateTimeDateWithoutSecond(value))
                return;

            data.Value = value;
            unitOfWork.EIOProcedureSummaryDataRepository.Update(data);
        }

        protected bool ConfirmProcedureSummary(EIOProcedureSummary procedure, User user, string kind)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            
            if (kind == "Surgoen" && positions.Contains("Doctor") && procedure.ProcedureDoctorId == null)            {
                procedure.ProcedureDoctorId = user?.Id;
                procedure.ProcedureTime = DateTime.Now;
            }
            //else if (kind == "Head Of Department" && positions.Contains("Head Of Department") && procedure.HeadOfDepartmentId == null)
            //{
            //    procedure.HeadOfDepartmentId = user?.Id;
            //    procedure.HeadOfDepartmentTime = DateTime.Now;
            //}
            //else if (kind == "Director" && positions.Contains("Director") && procedure.DirectorId == null)
            //{
            //    procedure.DirectorId = user?.Id;
            //    procedure.DirectorTime = DateTime.Now;
            //}
            else
                return false;

            unitOfWork.EIOProcedureSummaryRepository.Update(procedure);
            unitOfWork.Commit();
            return true;
        }
    }
}
