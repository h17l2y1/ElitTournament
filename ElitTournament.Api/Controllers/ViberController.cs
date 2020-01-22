using ElitTournament.Domain.Providers.Interfaces;
using ElitTournament.Domain.Services.Interfaces;
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

		public async Task<IActionResult> Update()
		{
			return Ok();
		}
	}
}
