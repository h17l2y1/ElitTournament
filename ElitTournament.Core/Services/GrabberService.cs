using System;
using ElitTournament.Core.Providers.Interfaces;
using ElitTournament.Core.Services.Interfaces;
using ElitTournament.Core.Views;
using ElitTournament.DAL.Entities;
using ElitTournament.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using CoreHtmlToImage;
using ElitTournament.Core.Helpers.Interfaces;


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
			List<League> tables = await _grabberProvider.GetLeagues();
			List<Schedule> schedule = await _grabberProvider.GetSchedule();
			// TODO: implement score
			//List<League> scores = await _grabberProvider.GetScores();

			DataVersion dataVersion = new DataVersion
			{
				Leagues = tables,
				Schedules = schedule
			};

			await _dataVersionRepository.CreateAsync(dataVersion);
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
