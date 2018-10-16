using System.Web.Http;
using solution.App_Start;

namespace solution
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            IocConfig.RegisterCastleWindsor();
        }
    }
}
