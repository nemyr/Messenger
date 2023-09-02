using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Message;

namespace MessengerAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class Message : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Send([FromQuery] MessageData messageData) {



            return Ok();
        }
    }
}
