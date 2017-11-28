using System.Web.Http;

namespace MyPlayground.Collector.App.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        [Route]
        public IHttpActionResult Get() => this.Ok("Ready");
    }
}