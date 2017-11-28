using System.Threading.Tasks;
using System.Web.Cors;
using Microsoft.Owin.Cors;
using Owin;

namespace MyPlayground.Collector.App
{
    public static class CorsConfig
    {
        public static void RegisterCors(this IAppBuilder app)
        {
            var corsPolicy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true,
                AllowAnyOrigin = true
            };

            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(corsPolicy)
                }
            };

            app.UseCors(corsOptions);
        }
    }
}