using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Ninject.Web.WebApi.OwinHost;
using System.Web.Http;
using Ninject.Web.Common.OwinHost;
using SubscribersSystem.WebApplication.Bootstrap;
using System.Web.Http.Cors;

namespace SubscribersSystem.WebApplication
{
    public class OwinBootstrap
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.EnableCors(new EnableCorsAttribute("*", "*", "*", "X-Custom-Header"));
            config.MapHttpAttributeRoutes(); //enables route mapping attributes
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional}
                );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            appBuilder.UseNinjectMiddleware(new NinjectBootstrap().GetKernel).UseNinjectWebApi(config);
        }
    }
}
