using EForm.Filters;
using System.Configuration;
using System.Net.Http.Headers;
using System.Web.Http;

namespace EForm
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            if (ConfigurationManager.AppSettings["HiddenError"].Equals("true"))
            {
                config.Filters.Add(new CustomExceptionFilter());
                config.MessageHandlers.Add(new CustomModifyingErrorMessageDelegatingHandler());
            }

            // Configure JSON 
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
