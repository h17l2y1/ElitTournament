using ElitTournament.Domain.Services.Interfaces;
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
		public async Task<IActionResult> GrabbSchedule()
		{
			string result = await _service.GrabbSchedule();
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateSchedule()
		{
			string result = await _service.UpdateSchedule();
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GrabbLeagues()
		{
			var result = await _service.GrabbLeagues();
			return Ok(result);
		}
	}
}