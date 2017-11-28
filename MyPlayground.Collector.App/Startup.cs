using System.Web.Http;
using Microsoft.Owin;
using MyPlayground.Collector.App;
using Newtonsoft.Json.Serialization;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

[assembly: OwinStartup(typeof(Startup))]

namespace MyPlayground.Collector.App
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            this.RegisterDependencies(container);

            container.Verify();

            app.Use(async (context, next) =>
            {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    await next();
                }
            });

            var config = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };

            var jsonFormatter = config.Formatters.JsonFormatter;

            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            app.RegisterCors(); // This should happen before call to app.UseWebApi(config);

            app.UseWebApi(config);

            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
        }

        internal void RegisterDependencies(Container container)
        {

        }
    }
}