using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cdc.Nndss.Fhir.Web.Controllers
{
    /// <summary>
    /// Index route controller class
    /// </summary>
    [ApiController]
    [Route("api")]
    [AllowAnonymous]
    public sealed class IndexController : ControllerBase
    {
        private readonly string _version = typeof(Startup).Assembly.GetName().Version?.ToString() ?? "no assembly version is available";

        // GET api/1.0
        /// <summary>
        /// Gets the version number of this web service
        /// </summary>
        /// <returns>Version number</returns>
        [HttpGet]
        [Produces("application/json")]
        public IActionResult Index()
        {
            return Ok(new { Version = _version });
        }
    }
}