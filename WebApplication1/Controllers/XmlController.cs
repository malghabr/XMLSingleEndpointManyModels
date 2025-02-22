using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/xml")]
    public class XmlController : ControllerBase
    {
        [HttpPost("process")]
        public IActionResult ProcessXml([ModelBinder(BinderType = typeof(XmlModelBinder))] object model)
        {
            if (model is catalog catalog)
            {
                // Process catalog
                return Ok(catalog);
            }
            else if (model is Employees employees)
            {
                // Process employees
                return Ok(employees);
            }
            else if (model is note note)
            {
                // Process note
                return Ok(note);
            }
            else
            {
                return BadRequest("Invalid XML input.");
            }
        }
    }
}

