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
		private readonly IDataVersionRepository _dataVersionRepository;

		public GrabberService(IGrabberProvider scheduleProvider, IScheduleRepository scheduleRepository, ILeagueRepository leagueRepository,
			IDataVersionRepository dataVersionRepository)
		{
			_scheduleRepository = scheduleRepository;
			_leagueRepository = leagueRepository;
			_grabberProvider = scheduleProvider;
			_dataVersionRepository = dataVersionRepository;
		}

		public async Task GrabbElitTournament()
		{
			List<Schedule> schedule = await _grabberProvider.GetSchedule();
			List<League> leagues = await _grabberProvider.GetLeagues();

			var c = new DataVersion
			{
				Schedules = schedule,
				Leagues = leagues
			};

			await _dataVersionRepository.CreateAsync(c);
			//await _scheduleRepository.CreateAsync(schedule);
			//await _leagueRepository.CreateAsync(leagues);
		}

		public async Task<GrabbElitTournamentView> GetElitTournament()
		{
			int version = await _dataVersionRepository.GetLastVersion();
			IEnumerable<Schedule> schedule = await _scheduleRepository.GetAll(version);
			IEnumerable<League> leagues = await _leagueRepository.GetAll(version);

			GrabbElitTournamentView result = new GrabbElitTournamentView(schedule, leagues);

			return result;
		}

		public async Task<string> FindGame(string team)
		{
			string result = await _scheduleRepository.FindGame(team);
			return result;
		}

	}
}
