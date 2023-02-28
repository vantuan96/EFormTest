using EForm.Authentication;
using EForm.BaseControllers;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class SuportAdminController : BaseApiController
    {
        [HttpGet]
        [Route("api/admin/AcceptHandOverCheckList/{visit_type}/{visit_id}")]
        [Permission(Code = "ADMINACCEPTHANDOVERCHECKLIST")]
        public IHttpActionResult AcceptHandOverCheckList(Guid visit_id, string visit_type)
        {
            if (visit_type == "ED") { 
                var ed = GetED(visit_id);
                var edhand = unitOfWork.HandOverCheckListRepository.FirstOrDefault(e => e.Id == ed.HandOverCheckListId);
                edhand.IsAcceptNurse = true;
                edhand.IsAcceptPhysician = true;
                unitOfWork.Commit();
            }

            if (visit_type == "IPD")
            {
                var ipd = GetIPD(visit_id);
                var ipdhand = unitOfWork.IPDHandOverCheckListRepository.FirstOrDefault(e => e.Id == ipd.HandOverCheckListId);
                ipdhand.IsAcceptNurse = true;
                ipdhand.IsAcceptPhysician = true;
                unitOfWork.Commit();
            }
                
            if (visit_type == "OPD")
            {
                var opd = GetOPD(visit_id);
                var opdhand = unitOfWork.OPDHandOverCheckListRepository.FirstOrDefault(e => e.Id == opd.OPDHandOverCheckListId);
                opdhand.IsAcceptNurse = true;
                opdhand.IsAcceptPhysician = true;
                unitOfWork.Commit();
            }

            if (visit_type == "EOC")
            {
                var eoc = GetEOC(visit_id);
                var eochand = unitOfWork.EOCHandOverCheckListRepository.FirstOrDefault(e => e.VisitId == eoc.Id);
                eochand.IsAcceptNurse = true;
                eochand.IsAcceptPhysician = true;
                unitOfWork.Commit();
            }


            return Content(HttpStatusCode.BadRequest, new
            {
                Message = "Không có thông tin"
            });
        }
    }
}
