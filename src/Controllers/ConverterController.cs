using System.Threading.Tasks;
using Hl7.Fhir.Model;
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
    public sealed class ConverterController : ControllerBase
    {
        // POST api/convert
        /// <summary>
        /// Converts an HL7 v2.5.1 message to an HL7 FHIR resource
        /// </summary>
        /// <returns>FHIR representation of the HL7 v2.5.1 message</returns>
        [HttpPost("convert")]
        [Consumes("text/plain")]
        [Produces("application/json")]
        public async Task<IActionResult> Convert([FromBody] string message)
        {
            Converter converter = new Converter();
            Patient patient = new Patient();

            await System.Threading.Tasks.Task.Run( () => 
            {
                var resource = converter.Convert(message);
                patient = resource;
            });

            return Ok(patient);
        }
    }
}