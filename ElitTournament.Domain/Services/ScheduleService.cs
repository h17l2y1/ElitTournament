using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Services.Interfaces;
using System.Collections.Generic;

namespace ElitTournament.Domain.Services
{
	public class ScheduleService : IScheduleService
	{
		private readonly ICacheHelper _сacheHelper;

		public ScheduleService(ICacheHelper сacheHelper)
		{
			_сacheHelper = сacheHelper;
		}

		public string FindGame(string teamName)
		{
			List<Schedule> schedule = _сacheHelper.Get();
			if (schedule == null)
			{
				return "Cache is empty";
			}

			foreach (var place in schedule)
			{
				string a = place.Place;
				foreach (var game in place.Games)
				{
					if (game.Contains(teamName))
					{
						return $"{place.Place} {game}";
					}
				}
			}
			return "Команда не найдена";
		}

	}
}
