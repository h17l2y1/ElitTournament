using ElitTournament.Core.Services.Interfaces;
using ElitTournament.Core.Views;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElitTournament.Telegram.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class GrabbController : ControllerBase
	{
		private readonly IGrabberService _service;

		public GrabbController(IGrabberService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GrabbElitTournament()
		{
			await _service.GrabbElitTournament();
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetElitTournament()
		{
			GrabbElitTournamentView result = _service.GetElitTournament();
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> FindGame(string team)
		{
			string result = _service.FindGame(team);
			return Ok(result);
		}

	}
}