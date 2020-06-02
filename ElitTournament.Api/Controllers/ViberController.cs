using ElitTournament.Domain.Providers;
using ElitTournament.Domain.Providers.Interfaces;
using ElitTournament.Domain.Services.Interfaces;
using ElitTournament.Domain.Views;
using ElitTournament.Domain.Views.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ElitTournament.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ViberController : ControllerBase
	{
		private readonly IViberProvider _service;

		public ViberController(IViberProvider service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> Set()
		{
			var res = await _service.SetWebHookToken();
			return Ok(res);
		}

		[HttpGet]
		public async Task<IActionResult> GetAccountInfo()
		{
			var res = await _service.GetAccountInfo();
			return Ok(res);
		}

		[HttpGet]
		public async Task<IActionResult> SendTextMessage(string text)
		{
			var res = await _service.SendTextMessage(text);
			return Ok(res);
		}


		[HttpPost]
		public async Task<IActionResult> Update([FromBody]RootObject callBack)
		{
			try
			{
				if (callBack.Event == ViberEventEnum.webhook.ToString())
				{
					return Ok();
				}
				await _service.Update(callBack);
				return Ok();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}
