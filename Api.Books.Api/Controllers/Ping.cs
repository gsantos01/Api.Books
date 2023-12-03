using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ping : ControllerBase
    {

        [HttpGet]
        public ActionResult HealfCheck()
        {
            return Ok(new { response = "Pong" });
        }
    }
}
