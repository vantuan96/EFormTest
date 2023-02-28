using Admin.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Common
{
    public class MasterDatas
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
                
        public List<ActionViewModel> GetListActionByRoleId(Guid id)
        {
            var lstExist = unitOfWork.RoleActionRepository.Find(x => !x.IsDeleted && x.RoleId == id).Select(x => x.ActionId).ToList();
            var result = unitOfWork.ActionRepository.Find(x => !x.IsDeleted).OrderBy(x => x.Name).Select(x => new ActionViewModel { 
                Id = x.Id,
                RoleId = id,
                Name = x.Name,
                IsChecked = lstExist.Contains(x.Id)
            }).ToList();

            return result;
        }

        public List<RoleViewModel> GetListRole()
        {
            var result = unitOfWork.RoleRepository.Find(x => !x.IsDeleted).Select(x => new RoleViewModel //OrderBy(x => x.Site.Name).ThenBy(x => x.EnName)
            {
                Id = x.Id,
                Hospital = "", //x.Site.Name
                Role = x.EnName
            }).ToList();

            return result;
        }

        public List<RoleViewModel> GetListRoleByUserId(Guid id)
        {
            var lstRole = unitOfWork.UserRoleRepository.Find(x => !x.IsDeleted && x.UserId == id).Select(x => x.RoleId).ToList();
            var result = unitOfWork.RoleRepository.Find(x => !x.IsDeleted).Select(x => new RoleViewModel // .OrderBy(x => x.Site.Name).ThenBy(x => x.EnName)
            {
                Id = x.Id,
                UserId = id,
                Hospital = "",//x.Site.Name
                Role = x.EnName,
                IsChecked = lstRole.Contains(x.Id)
            }).ToList();

            return result;
        }

        public List<UserViewModel> GetListUser()
        {
            var result = unitOfWork.UserRepository.Find(x => !x.IsDeleted).OrderBy(x => x.Username).Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.Username,
                DisplayName = x.DisplayName,
                Title = x.Title
            }).ToList();

            return result;
        }

        #region Dropdown Lists
        public List<SelectListItem> GetListSites()
        {
            var result = unitOfWork.SiteRepository.AsQueryable().Where(s => !s.IsDeleted).OrderBy(x => x.Name).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            
            return result;
        }

        public List<SelectListItem> GetListVisitTypes()
        {
            var result = unitOfWork.VisitTypeGroupRepository.AsQueryable().Where(s => !s.IsDeleted).OrderBy(x => x.ViName).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.ViName
            }).ToList();
            
            return result;
        }

        public List<SelectListItem> GetListActions(string visitTypeId = null)
        {
            Guid visitId;
            var listActions = unitOfWork.ActionRepository.AsQueryable().Where(s => !s.IsDeleted);
            if (!string.IsNullOrEmpty(visitTypeId) && Guid.TryParse(visitTypeId, out visitId))
                listActions = listActions.Where(s => s.VisitTypeGroupId == visitId || !s.VisitTypeGroupId.HasValue);
            listActions = listActions.OrderBy(x => x.Name);
            var result = listActions.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            return result;
        }

        public List<SelectListItem> GetListSpecialties(string visitTypeId = null, string visit_type_code = null)
        {
            Guid visitId;
            var listSpecialties = unitOfWork.SpecialtyRepository.Include("Site.Name").Include("VisitTypeGroup.Code").Where(s => !s.IsDeleted);
            if (!string.IsNullOrEmpty(visitTypeId) && Guid.TryParse(visitTypeId, out visitId))
            {
                listSpecialties = listSpecialties.Where(s => s.VisitTypeGroupId == visitId);
            }
            if (!string.IsNullOrEmpty(visit_type_code))
            {
                listSpecialties = listSpecialties.Where(s => s.VisitTypeGroup.Code == visit_type_code);
            }
            listSpecialties = listSpecialties.OrderBy(x => x.ViName);
            var result = listSpecialties.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Site.ApiCode + " " + x.VisitTypeGroup.Code+ " " + x.ViName
            }).ToList();

            return result;
        }

        public List<SelectListItem> GetListPositions()
        {
            var result = unitOfWork.PositionRepository.AsQueryable().Where(s => !s.IsDeleted).OrderBy(x => x.ViName).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.ViName
            }).ToList();

            return result;
        }

        public List<SelectListItem> GetListRoles()
        {
            var result = unitOfWork.RoleRepository.AsQueryable().Where(s => !s.IsDeleted).OrderBy(x => x.ViName).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.ViName
            }).ToList();

            return result;
        }

        public List<SelectListItem> GetListAdminRoles()
        {
            var result = unitOfWork.AdminRoleRepository.AsQueryable().OrderBy(x => x.RoleName).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.RoleDescription
            }).ToList();

            return result;
        }
        #endregion
    }
}