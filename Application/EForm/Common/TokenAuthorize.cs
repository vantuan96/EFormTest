using EForm.MemCached;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace EForm.Common
{
    public class TokenAuthorize: AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext) 
        {
            try
            {
                string token = actionContext.Request.Headers.GetValues("Authorization").ElementAt(0).Replace(" ", "").Replace("Bearer", "");
                return !MemcachedToken.IsTokenInBlackList(token);
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}