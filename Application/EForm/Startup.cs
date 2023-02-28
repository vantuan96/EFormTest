using Microsoft.Owin;
using Owin;
using System.Web.Http;
using EForm.Middlewares;
using Microsoft.Owin.Security.Cookies;
using DataAccess.Repository;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Globalization;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.Notifications;
using System.Net;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security.DataHandler;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web;

using Microsoft.Owin.Security.DataProtection;
using Microsoft.IdentityModel.Protocols;
using System.IO;
using System.Web.Helpers;
using System.IdentityModel.Tokens;
using System.Management;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

[assembly: OwinStartup(typeof(EForm.Startup))]

namespace EForm
{
    public class Startup
    {
        private static string redirectUri = ConfigurationManager.AppSettings["RedirectUri"];
        private static string clientId = ConfigurationManager.AppSettings["ClientId"];
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["PostLogoutRedirectUri"];
        private static string cookieName = CookieAuthenticationDefaults.CookiePrefix + CookieAuthenticationDefaults.AuthenticationType;
        private static string metadata = string.Format("{0}/{1}/federationmetadata/2007-06/federationmetadata.xml", aadInstance, tenant);
        string authority = string.Format("{0}{1}", aadInstance, tenant);
        public static readonly string Authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);
        public static string urltemp = string.Empty;
        public static string token = string.Empty;
        public static string Unique = "";

        public static string sessionId = string.Empty;
        public static int isLogin = 0;
        public static string Email = "";
        public static string PostLogoutRedirectUri
        {
            get { return postLogoutRedirectUri; }
        }
        public static string Uniques
        {
            get { return Unique; }
        }
        public static string AADInstance
        {
            get { return aadInstance; }
        }

        public static string ClientId
        {
            get { return clientId; }
        }

        public static string CheckSessionIFrame
        {
            get;
            set;
        }

        public static string RedirectUri
        {
            get { return redirectUri; }
        }

        public static TicketDataFormat ticketDataFormat
        {
            get;
            set;
        }
        public static int IsLogin
        {
            get; set;
        }
        public static string SessionId
        {
            get { return sessionId; }
        }
        public static string email
        {
            get { return Email; }
        }
        public static string CookieName
        {
            get { return cookieName; }
        }
        public static string UrlAzure
        {
            get { return urltemp; }
        }
        public static string Token
        {
            get { return token; }
        }
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
            //IdentityModelEventSource.ShowPII = true;
            //ServicesPointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3; // only allow TLSV1.2 and SSL3
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            CookieAuthenticationOptions cookieOptions = new CookieAuthenticationOptions
            {
                CookieName = CookieName,
            };

            app.UseOpenIdConnectAuthentication(
          new OpenIdConnectAuthenticationOptions
          {
              // Sets the ClientId, authority, RedirectUri as obtained from web.config
              ClientId = ClientId,
              Authority = Authority,
              RedirectUri = RedirectUri,


              // PostLogoutRedirectUri is the page that users will be redirected to after sign-out. In this case, it is using the home page
              PostLogoutRedirectUri = PostLogoutRedirectUri,

              TokenValidationParameters = new TokenValidationParameters
              {
                  //SaveSigninToken = true,
                  ValidateIssuer = false,
              },
              BackchannelHttpHandler = new HttpClientHandler
              {
                  UseProxy = true,
                  Proxy = new WebProxy
                  {

                      Address = new Uri("http://10.115.50.11:9090")
                  }
              },
              // OpenIdConnectAuthenticationNotifications configures OWIN to send notification of failed authentications to OnAuthenticationFailed method
              Notifications =
              new OpenIdConnectAuthenticationNotifications
              {
                  AuthorizationCodeReceived = AuthorizationCodeRecieved,
                  AuthenticationFailed = Startup.AuthenticationFailed,
                  RedirectToIdentityProvider = Startup.RedirectToIdentityProvider,
                  SecurityTokenValidated = Startup.SecurityTokenValidated,
              },
          }
      );

            // Enable CORS (cross origin resource sharing) for making request using browser from different domains
            if (ConfigurationManager.AppSettings["HiddenError"].Equals("false"))
                app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //Cookie authenicate
            var session_timeout = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["SessionTimeout"]);
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                CookieName = "EFormED",
                SlidingExpiration = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(session_timeout),
                Provider = new CookieAuthenticationProvider
                {
                    OnResponseSignedIn = context =>
                    {
                        var cookies = context.Response.Headers.GetCommaSeparatedValues("Set-Cookie");
                        var cookieValue = GetAuthenCookie(cookies);

                        if (!string.IsNullOrEmpty(cookieValue))
                            UpdateSession(context.Identity.Name, cookieValue);
                    }
                }
            });
            IDataProtector dataProtector = app.CreateDataProtector(
               typeof(CookieAuthenticationMiddleware).FullName,
               cookieOptions.AuthenticationType, "v1");
            ticketDataFormat = new TicketDataFormat(dataProtector);
            //Log middleware
            //app.Use(typeof(LogChangeMiddleware));

            //HttpConfiguration config = new HttpConfiguration();
            //WebApiConfig.Register(config);
        }

        private async static Task SecurityTokenValidated(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            OpenIdConnectAuthenticationOptions tenantSpecificOptions = new OpenIdConnectAuthenticationOptions();
            tenantSpecificOptions.BackchannelHttpHandler = new HttpClientHandler
            {
                UseProxy = true,
                Proxy = new WebProxy
                {

                    Address = new Uri("http://10.115.50.11:9090")
                }
            };
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = new WebProxy
                {

                    Address = new Uri("http://10.115.50.11:9090")
                }
            };
            tenantSpecificOptions.Authority = string.Format(aadInstance, notification.AuthenticationTicket.Identity.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value);
          
            tenantSpecificOptions.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(tenantSpecificOptions.Authority + "/.well-known/openid-configuration",

            new HttpClient(handler: httpClientHandler, disposeHandler: true));
            OpenIdConnectConfiguration tenantSpecificConfig = await tenantSpecificOptions.ConfigurationManager.GetConfigurationAsync(notification.Request.CallCancelled);
            notification.AuthenticationTicket.Identity.AddClaim(new Claim("issEndpoint", tenantSpecificConfig.AuthorizationEndpoint, ClaimValueTypes.String, "EForm"));
            token = notification.ProtocolMessage.Code;

            sessionId = notification.ProtocolMessage.SessionState;
            Email = notification.AuthenticationTicket.Identity.Name;
            CheckSessionIFrame = notification.AuthenticationTicket.Properties.Dictionary[OpenIdConnectSessionProperties.CheckSessionIFrame];
            return;
        }

        private static Task AuthenticationFailed(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {

            string cookieStateValue = null;
            ICookieManager cookieManager = new ChunkingCookieManager();
            string cookie = cookieManager.GetRequestCookie(notification.OwinContext, CookieName);
            AuthenticationTicket ticket = ticketDataFormat.Unprotect(cookie);
            if (ticket?.Properties.Dictionary != null)
                ticket.Properties.Dictionary.TryGetValue(OpenIdConnectAuthenticationDefaults.AuthenticationType + "SingleSignOut", out cookieStateValue);

            // If the failed authentication was a result of a request by the SingleSignOut javascript
            if (cookieStateValue != null && cookieStateValue.Contains(notification.ProtocolMessage.State) && notification.Exception.Message == "login_required")
            {
                // Clear the SingleSignOut cookie, and clear the OIDC session state so 
                //that we don't see any further "Session Changed" messages from the iframe.
                ticket.Properties.Dictionary[OpenIdConnectSessionProperties.SessionState] = "";
                ticket.Properties.Dictionary[OpenIdConnectAuthenticationDefaults.AuthenticationType + "SingleSignOut"] = "";
                cookieManager.AppendResponseCookie(notification.OwinContext, CookieName, ticketDataFormat.Protect(ticket), new CookieOptions());

                notification.Response.Redirect("Account/SingleSignOut");
                notification.HandleResponse();
            }

            if (ticket == null)
            {
                if (notification.Exception.Message == "login_required")
                {
                    notification.Response.Redirect("Authen/ReLogin");
                    notification.HandleResponse();
                }
                else
                {
                    notification.Response.Redirect("Authen/SignIn");
                    notification.HandleResponse();
                }


            }

            //else
            //{
            //    notification.Response.Redirect("Authen/SignIn");
            //    notification.HandleResponse();

            //}
            //notification.Response.Redirect("Authen/Login");
            //notification.HandleResponse();
            return Task.FromResult<object>(null);

        }

        public static Task RedirectToIdentityProvider(RedirectToIdentityProviderNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            // If a challenge was issued by the SingleSignOut javascript
            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            if (notification.Request.Uri.AbsolutePath == url.Action("SessionChanged", "Authen"))
            {

                // Store the state in the cookie so we can distinguish OIDC messages that occurred
                // as a result of the SingleSignOut javascript.
                ICookieManager cookieManager = new ChunkingCookieManager();
                string cookie = cookieManager.GetRequestCookie(notification.OwinContext, CookieName);

                AuthenticationTicket ticket = ticketDataFormat.Unprotect(cookie);
                if (ticket?.Properties.Dictionary != null)
                {
                    ticket.Properties.Dictionary[OpenIdConnectAuthenticationDefaults.AuthenticationType + "SingleSignOut"] = notification.ProtocolMessage.State;
                    cookieManager.AppendResponseCookie(notification.OwinContext, CookieName, ticketDataFormat.Protect(ticket), new CookieOptions());
                    notification.ProtocolMessage.Prompt = "none";
                    notification.ProtocolMessage.IssuerAddress = notification.OwinContext.Authentication.User.FindFirst("issEndpoint").Value;

                }
                // Return prompt=none request (to tenant specific endpoint) to SessionChanged controller.
                

                string redirectUrl = notification.ProtocolMessage.BuildRedirectUrl();
                notification.Response.Redirect(url.Action("SignOut", "Authen") + "?" + redirectUrl);
                notification.HandleResponse();
            }
            if (notification.Request.Uri.AbsolutePath == url.Action("SignIn1", "Authen"))
            {

                // Store the state in the cookie so we can distinguish OIDC messages that occurred
                // as a result of the SingleSignOut javascript.
                ICookieManager cookieManager = new ChunkingCookieManager();
                string cookie = cookieManager.GetRequestCookie(notification.OwinContext, CookieName);

                AuthenticationTicket ticket = ticketDataFormat.Unprotect(cookie);
                if (ticket?.Properties.Dictionary != null)
                {
                    ticket.Properties.Dictionary[OpenIdConnectAuthenticationDefaults.AuthenticationType + "SingleSignOut"] = notification.ProtocolMessage.State;
                    cookieManager.AppendResponseCookie(notification.OwinContext, CookieName, ticketDataFormat.Protect(ticket), new CookieOptions());


                }
                // Return prompt=none request (to tenant specific endpoint) to SessionChanged controller.
                notification.ProtocolMessage.Prompt = "select_account";
                notification.ProtocolMessage.IssuerAddress = notification.OwinContext.Authentication.User.FindFirst("issEndpoint").Value;

                string redirectUrl = notification.ProtocolMessage.BuildRedirectUrl();

                urltemp = redirectUrl;
                notification.Response.Redirect(redirectUrl);
                notification.HandleResponse();
            }
            return Task.FromResult<object>(null);
        }


        public Task AuthorizationCodeRecieved(AuthorizationCodeReceivedNotification notification)
        {
            // If the successful authorize request was issued by the SingleSignOut javascript
            if (notification.AuthenticationTicket.Properties.RedirectUri.Contains("SessionChanged"))
            {
                // Clear the SingleSignOut Cookie
                ICookieManager cookieManager = new ChunkingCookieManager();
                string cookie = cookieManager.GetRequestCookie(notification.OwinContext, CookieName);
                AuthenticationTicket ticket = ticketDataFormat.Unprotect(cookie);
                if (ticket.Properties.Dictionary != null)
                    ticket.Properties.Dictionary[OpenIdConnectAuthenticationDefaults.AuthenticationType + "SingleSignOut"] = "";
                cookieManager.AppendResponseCookie(notification.OwinContext, CookieName, ticketDataFormat.Protect(ticket), new CookieOptions());

                Claim existingUserObjectId = notification.OwinContext.Authentication.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier");
                Claim incomingUserObjectId = notification.AuthenticationTicket.Identity.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier");

                if (existingUserObjectId.Value != null && incomingUserObjectId != null)
                {
                    // If a different user is logged into AAD
                    if (existingUserObjectId.Value != incomingUserObjectId.Value)
                    {
                        // No need to clear the session state here. It has already been
                        // updated with the new user's session state in SecurityTokenValidated.
                        notification.Response.Redirect("Account/SingleSignOut");
                        notification.HandleResponse();
                    }
                    // If the same user is logged into AAD
                    else if (existingUserObjectId.Value == incomingUserObjectId.Value)
                    {
                        // No need to clear the session state, SecurityTokenValidated will do so.
                        // Simply redirect the iframe to a page other than SingleSignOut to reset
                        // the timer in the javascript.
                        notification.Response.Redirect("/");
                        notification.HandleResponse();
                    }
                }
            }

            return Task.FromResult<object>(null);
        }


        private string GetAuthenCookie(IList<string> cookies)
        {
            var cookieValue = "";
            var cookieValue1 = "";
            var cookieValue2 = "";
            var cookieValue3 = "";
            var cookieValue4 = "";
            foreach (var cookie in cookies)
            {
                var cookieKeyIndex = cookie.Contains("EFormED") ? 1 : -1;
                var cookieKeyIndex1 = cookie.IndexOf("EFormEDC1");
                var cookieKeyIndex2 = cookie.IndexOf("EFormEDC2");
                if (cookies.Count > 1)
                {
                    if (cookieKeyIndex1 != -1)
                    {
                        cookieValue2 = cookie.Substring("EFormEDC1".Length + 1);
                    }
                    if (cookieKeyIndex2 != -1)
                    {
                        // Add extra character for '='
                        cookieValue3 = cookie.Substring("EFormEDC2".Length + 1);
                    }
                    if (cookieKeyIndex == 1)
                    {
                        cookieValue4 = cookie.Substring("EFormED".Length + 1);
                    }
                }
                else
                if (cookieKeyIndex != -1)
                {
                    cookieValue1 = cookie.Substring("EFormED".Length + 1);
                }
                cookieValue = cookieValue1 + cookieValue2 + cookieValue3 + cookieValue4;
            }
            return cookieValue;
        }
        private void UpdateSession(string username, string cookieValue)
        {
            try
            {
                using (var unitOfWork = new EfUnitOfWork())
                {
                    var user = unitOfWork.UserRepository.FirstOrDefault(m => !m.IsDeleted && m.Username == username);
                    user.SessionId = cookieValue.Substring(0, 20);
                    user.Session = cookieValue;
                    unitOfWork.UserRepository.Update(user);
                    unitOfWork.Commit();
                }
            }
            catch (Exception) { }
        }
    }
}
