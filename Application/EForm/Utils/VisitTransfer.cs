using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace EForm.Utils
{
    public class VisitTransfer
    {
        private IUnitOfWork unitOfWork;
        private Specialty specialty;
        public VisitTransfer(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public bool IsExist(Guid? current_hocl_id, Guid? visit_id)
        {
            var ed_visit = unitOfWork.EDRepository.FirstOrDefault(e => !e.IsDeleted && e.IsTransfer && e.TransferFromId == current_hocl_id);
            if (ed_visit != null)
            {
                this.specialty = ed_visit.Specialty;
                return true;
            }

            var opd_visit = unitOfWork.OPDRepository.FirstOrDefault(e => !e.IsDeleted && e.IsTransfer && e.TransferFromId == current_hocl_id);
            if (opd_visit != null)
            {
                this.specialty = opd_visit.Specialty;
                return true;
            }

            var ipd_visit = unitOfWork.IPDRepository.FirstOrDefault(e => !e.IsDeleted && e.IsTransfer && e.TransferFromId == current_hocl_id);
            if (ipd_visit != null)
            {
                this.specialty = ipd_visit.Specialty;
                return true;
            }
            var wr_status = GetStatusIdByCode("EOCIH");
            var eoc_visit = unitOfWork.EOCRepository.FirstOrDefault(e => !e.IsDeleted && e.IsTransfer && e.TransferFromId == visit_id && e.StatusId == wr_status);
            if (eoc_visit != null)
            {
                this.specialty = eoc_visit.Specialty;
                return true;
            }
            this.specialty = null;
            return false;
        }
        private Guid GetStatusIdByCode(string code)
        {
            return unitOfWork.EDStatusRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.Code == code
            ).Id;
        }
        public dynamic BuildMessage()
        {
            var en_mes = $"Patient was transfered to {specialty?.EnName} so you can NOT change status";
            var vi_mes = $"Bệnh nhân đã chuyển đến {specialty?.ViName} nên không thể chuyển trạng thái";
            return new
            {
                Code = HttpStatusCode.BadRequest,
                Message = new
                {
                    EnMessage = en_mes,
                    ViMessage = vi_mes
                }
            };
        }
    }
}