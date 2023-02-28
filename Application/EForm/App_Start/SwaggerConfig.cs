using System.Web.Http;
using WebActivatorEx;
using EForm;

using System.Configuration;
using System.Linq;

namespace EForm
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            if (ConfigurationManager.AppSettings["HiddenError"].Equals("false"))
            {
                var thisAssembly = typeof(SwaggerConfig).Assembly;
            }
        }
    }
}
