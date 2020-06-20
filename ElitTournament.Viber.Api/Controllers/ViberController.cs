using System.Threading.Tasks;
using ElitTournament.Viber.BLL.Services.Interfaces;
using ElitTournament.Viber.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElitTournament.Viber.Api.Controllers
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
		public async Task<IActionResult> Update([FromBody]Callback callBack)
		{
			await _service.Update(callBack);
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			var result = await _service.GetAllUsers();
			return Ok(result);
		}
		
		[HttpGet]
		public async Task<IActionResult> SendBroadcastMessage()
		{
			await _service.SendBroadcastMessage();
			return Ok();
		}

	}
}
