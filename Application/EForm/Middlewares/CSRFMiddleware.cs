using DataAccess.Repository;
using EForm.Common;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace EForm.Middlewares
{
    public class CSRFMiddleware: OwinMiddleware
    {
        public CSRFMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            if (context.Request.Method.Equals("post", StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    //string cookieToken = context.Request.Cookies["__RequestVerificationToken"];
                    string formToken = context.Request.Headers["RequestVerificationToken"];
                    Debug.WriteLine(formToken);
                    if (CSRFToken.IsValid(formToken))
                    {
                        await Next.Invoke(context);
                    }
                    else
                    {
                        Debug.WriteLine("Invalid");
                        context.Response.StatusCode = 400;
                        context.Response.Body = Message.FORBIDDEN;
                    }
                    //Debug.WriteLine(cookieToken);
                    //Debug.WriteLine(formToken);
                    //Debug.WriteLine("HIHI");
                    //AntiForgery.Validate(cookieToken, formToken);
                }
                catch (Exception)
                {
                    Debug.WriteLine("Error");
                    context.Response.StatusCode = 400;
                    context.Response.Body = Message.FORBIDDEN;
                }
            }
            else
            {
                await Next.Invoke(context);
            }
        }
    }
}