using ElitTournament.Telegram.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ElitTournament.Telegram.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private ITelegramBotService _service;
        public TelegramController(ITelegramBotService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> SetWebHook()
        {
            await _service.SetWebhookAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody]Update update)
        {
            if (update == null)
            {
                throw new Exception("Invalid model");
            }

            if (update.Message.Text == null)
            {
                return Ok();
            }

            _service.Update(update);
            return Ok();
        }
    }
}
