using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.EOCModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using DataAccess.Repository;
using EForm.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Utils
{
    public class InHospital
    {
        private IUnitOfWork unitOfWork = new EfUnitOfWork();
        private List<Guid> in_hospital_status;
        private Guid? customer_id;
        private Guid? visit_id;
        private Guid? group_id;
        private string visit_code;

        public InHospital()
        {
            this.in_hospital_status = this.unitOfWork.EDStatusRepository.Find(
                e => !e.IsDeleted
                && Constant.InHospital.Contains(e.Code)
            ).Select(e => e.Id).ToList();
        }

        public void SetState(Guid customer_id, Guid? visit_id, Guid? group_id, string visit_code)
        {
            this.customer_id = customer_id;
            this.visit_id = visit_id;
            this.group_id = group_id;
            this.visit_code = visit_code;
        }
        private void ResetState()
        {
            this.customer_id = null;
            this.visit_id = null;
            this.group_id = null;
            this.visit_code = null;
        }
       
        public dynamic GetVisit()
        {
            if (this.customer_id == null)
                return null;

            var ed = GetEDVisit();
            if (ed != null)
            {
                ResetState();
                return ed;
            }

            var opd = GetOPDVisit();
            if (opd != null)
            {
                ResetState();
                return opd;
            }

            var ipd = GetIPDVisit();
            if (ipd != null)
            {
                ResetState();
                return ipd;
            }
            var eoc = GetEOCVisit();
            if (eoc != null)
            {
                ResetState();
                return eoc;
            }
            ResetState();
            return null;
        }
        private EOC GetEOCVisit() {
            var eocs = this.unitOfWork.EOCRepository.Find(
                    e => !e.IsDeleted &&
                    e.StatusId != null &&
                    this.in_hospital_status.Contains((Guid)e.StatusId) &&
                    e.CustomerId == this.customer_id
                );
            if (this.visit_id != null)
                eocs = eocs.Where(e => e.Id != this.visit_id);

            return eocs.FirstOrDefault();
        }
        private ED GetEDVisit()
        {
            var eds = this.unitOfWork.EDRepository.Find(
                e => !e.IsDeleted &&
                e.EDStatusId != null &&
                this.in_hospital_status.Contains((Guid)e.EDStatusId) &&
                e.CustomerId == this.customer_id
            );
            if (this.visit_id != null)
                eds = eds.Where(e => e.Id != this.visit_id);

            return eds.FirstOrDefault();
        }
        private OPD GetOPDVisit()
        {
            var opds = this.unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.EDStatusId != null &&
                this.in_hospital_status.Contains((Guid)e.EDStatusId) &&
                e.CustomerId == this.customer_id
            );

            if (this.visit_id != null)
                opds = opds.Where(e => e.Id != this.visit_id);

            if (this.group_id != null)
                opds = opds.Where(e => (e.GroupId == null || e.GroupId != group_id));

            return opds.FirstOrDefault();
        }
        private IPD GetIPDVisit()
        {
            var ipds = this.unitOfWork.IPDRepository.Find(
                e => !e.IsDeleted && !e.IsDraft &&
                e.EDStatusId != null &&
                this.in_hospital_status.Contains((Guid)e.EDStatusId) &&
                e.CustomerId == this.customer_id
            );

            if (this.visit_id != null)
                ipds = ipds.Where(e => e.Id != this.visit_id);

            // if (!string.IsNullOrEmpty(this.visit_code))
                // ipds = ipds.Where(e => !string.IsNullOrEmpty(e.VisitCode) && !e.VisitCode.Equals(this.visit_code));

            return ipds.FirstOrDefault();
        }

        public EDStatus GetStatus(string visit_type_group = "ED")
        {
            return unitOfWork.EDStatusRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                Constant.InHospital.Contains(e.Code)&&
                e.VisitTypeGroupId != null &&
                e.VisitTypeGroup.Code == visit_type_group
            );
        }
        public dynamic BuildErrorMessage(dynamic visit)
        {
            var local_spectialty = visit.Specialty;
            return new
            {
                ViMessage = $"Bệnh nhân hiện đang ở {local_spectialty?.ViName?.ToLower()} {local_spectialty?.Site?.Name}",
                EnMessage = $"The patient is in the {local_spectialty?.EnName?.ToLower()} {local_spectialty?.Site?.Name}",
                NeedShowMsg = true
            };
        }
    }
}