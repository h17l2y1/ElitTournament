using ElitTournament.Domain.Providers;
using ElitTournament.Domain.Providers.Interfaces;
using ElitTournament.Domain.Services.Interfaces;
using ElitTournament.Domain.Views;
using ElitTournament.Domain.Views.Enums;
using Microsoft.AspNetCore.Mvc;
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
			await _service.SetWebHook();
			return Ok();
		}

		//[HttpGet]
		//public async Task<IActionResult> Update(/*[FromBody]CallBack callBack*/)
		//{
		//	return Ok();
		//}
		[HttpPost]
		public async Task<IActionResult> Update([FromBody]RootObject callBack)
		{
            if(callBack.Event == ViberEventEnum.webhook.ToString())
            {
                return Ok();
            }
			await _service.Update(callBack);
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> Remove()
		{
			await _service.Remove();
			return Ok();
		}
	}
}
