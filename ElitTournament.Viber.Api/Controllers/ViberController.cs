using ElitTournament.Viber.BLL.Services.Interfaces;
using ElitTournament.Viber.BLL.View;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElitTournament.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ViberController : ControllerBase
	{
		private readonly IViberBotService _service;

		public ViberController(IViberBotService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> SetWebHook()
		{
			var res = await _service.SetWebHookAsync();
			return Ok(res);
		}

		[HttpGet]
		public async Task<IActionResult> GetAccountInfo()
		{
			var res = await _service.GetAccountInfo();
			return Ok(res);
		}

		[HttpPost]
		public async Task<IActionResult> Update([FromBody]RootObject callBack)
		{
			await _service.Update(callBack);
			return Ok();
		}

	}
}
