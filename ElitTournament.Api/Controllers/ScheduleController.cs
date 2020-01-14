using ElitTournament.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElitTournament.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ScheduleController : ControllerBase
	{
		private readonly IScheduleService _service;

		public ScheduleController(IScheduleService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> FindGame(string teamName)
		{
			var result = _service.FindGame(teamName);
			return Ok(result);
		}


	}
}