using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Message;

namespace MessengerAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        [HttpPost]
        public IActionResult Send(MessageData messageData) {

            return Ok();
        }

        [HttpPost]
        public IActionResult GetFromChat()
        {
            return Ok();
        }
    }
}
