using ElitTournament.Core.Providers.Interfaces;
using ElitTournament.Core.Services.Interfaces;
using ElitTournament.Core.Views;
using ElitTournament.DAL.Entities;
using ElitTournament.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Core.Services
{
	public class GrabberService : IGrabberService
	{
		private readonly IScheduleRepository _scheduleRepository;
		private readonly ILeagueRepository _leagueRepository;
		private readonly IGrabberProvider _grabberProvider;

		public GrabberService(IGrabberProvider scheduleProvider, IScheduleRepository scheduleRepository, ILeagueRepository leagueRepository)
		{
			_scheduleRepository = scheduleRepository;
			_leagueRepository = leagueRepository;
			_grabberProvider = scheduleProvider;
		}

		public async Task GrabbElitTournament()
		{
			List<Schedule> schedule = await _grabberProvider.GetSchedule();
			List<League> leagues = await _grabberProvider.GetLeagues();

			await _scheduleRepository.CreateAsync(schedule);
			await _leagueRepository.CreateAsync(leagues);
		}

		public async Task<GrabbElitTournamentView> GetElitTournament()
		{
			IEnumerable<Schedule> schedule = await _scheduleRepository.GetAll();
			IEnumerable<League> leagues = await _leagueRepository.GetAll();

			GrabbElitTournamentView result = new GrabbElitTournamentView(schedule, leagues);

			return result;
		}

		public async Task<string> FindGame(string team)
		{
			string result = await _scheduleRepository.FindGame(team);
			return result;
		}

		public async Task RemoveAll()
		{
			await _scheduleRepository.RemoveAsync()
			await _leagueRepository.RemoveAsync()
		}
	}
}
