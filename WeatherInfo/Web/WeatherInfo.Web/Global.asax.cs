using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Application.Map;
using System.Linq;

namespace WeatherInfo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(x =>
            {
                x.AddProfile<MapProfile>(); // application
                x.AddProfile<Location.Map.MapProfile>();
            });
        }

        protected void Application_BeginRequest()
        {
            if (Request.Headers.AllKeys.Contains("origin") && Request.HttpMethod == "OPTIONS")
            {
                //Response.Headers.Add("Access-Control-Allow-Origin", "*");
                Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With, Session");
                Response.Flush();
            }
        }

    }
}
