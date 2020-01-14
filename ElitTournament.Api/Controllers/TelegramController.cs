using ElitTournament.Domain.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ElitTournament.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private IBotProvider _botProvider;
        public TelegramController(IBotProvider botProvider)
        {
            _botProvider = botProvider;
        }

        [HttpPost]
        public async Task<ActionResult> Update([FromBody]Update update)
        {
            if (update == null)
            {
                throw new Exception("Invalid model");
            }
            if(update.Message.Text == null)
            {
                return Ok();
            }
            await _botProvider.Update(update);
            return Ok();
        }
    }
}
