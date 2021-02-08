using Microsoft.AspNetCore.Mvc;

namespace AccountMgmt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "pong";
        }
    }
}
