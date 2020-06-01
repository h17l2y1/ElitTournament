using ElitTournament.Domain.Services.Interfaces;
using ElitTournament.Domain.Views;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElitTournament.Api.Controllers
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

	}
}