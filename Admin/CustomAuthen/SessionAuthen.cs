using DataAccess.Repository;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Admin.CustomAuthen
{
    public class SessionAuthen : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthroized = base.AuthorizeCore(httpContext);
            if (!isAuthroized)
                return false;

            return IsTokenValid(httpContext);
        }

        private bool IsTokenValid(HttpContextBase httpContext)
        {
            try
            {
                string session = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                var session_id = session.Substring(0, 20);
                using (IUnitOfWork unitOfWork = new EfUnitOfWork())
                {
                    var user = unitOfWork.UserRepository.FirstOrDefault(
                        e => !e.IsDeleted &&
                        !string.IsNullOrEmpty(e.SessionId) &&
                        e.SessionId == session_id
                    );
                    return user != null;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}