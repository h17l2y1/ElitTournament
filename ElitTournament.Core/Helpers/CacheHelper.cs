using ElitTournament.Core.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace ElitTournament.Core.Helpers
{
	public class CacheHelper : ICacheHelper
	{
		private IMemoryCache _cache;
		private readonly string _scheduleKey = "1908f0292df1";
		private readonly string _leagueKey = "14da59f1006a";
		private readonly string _teamKey = "77fg21v0984c";
		public bool IsCacheExist { get; set; }

		public CacheHelper(IMemoryCache cache)
		{
			_cache = cache;
		}

		public void Update(List<Schedule> schedule, List<League> league)
		{
			Clear();
			SaveSchedule(schedule);
			SaveLeagues(league);
			SaveTeams(league);

			IsCacheExist = true;
		}

		public List<League> GetLeagues()
		{
			List<League> leagues = _cache.Get<List<League>>(_leagueKey);
			if (leagues == null)
			{
				return null;
			}

			return leagues;
		}

		public List<Schedule> GetSchedule()
		{
			List<Schedule> schedule = _cache.Get<List<Schedule>>(_scheduleKey);
			if (schedule == null)
			{
				return null;
			}

			return schedule;
		}

		public List<string> GetTeams()
		{
			List<string> teams = _cache.Get<List<string>>(_teamKey);
			if (teams != null)
			{
				return teams;
			}
			return null;
		}

		public string FindGame(string teamName)
		{
			List<Schedule> schedule = GetSchedule();
			if (schedule != null && schedule.Count != 0)
			{
				List<string> list = new List<string>();
				string teamWithSpace = teamName.Replace("-", " ").ToUpper();

				foreach (var place in schedule)
				{
					foreach (var game in place.Games)
					{
						string gameString = game.Replace("-", " ").ToUpper();
						if (gameString.Contains(teamWithSpace))
						{
							list.Add($"{place.Place}\n{game}");
						}
					}
				}

				if (list.Count != 0)
				{
					string scheduleString = String.Join("\n\n", list);
					return scheduleString;
				}
			}

			return null;
		}

		private void Clear()
		{
			_cache.Remove(_scheduleKey);
			_cache.Remove(_leagueKey);
			_cache.Remove(_teamKey);
		}

		private void SaveSchedule(List<Schedule> data)
		{
			List<Schedule> deck = data;
			if (!_cache.TryGetValue(_scheduleKey, out data))
			{
				data = deck;
				MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(8));
				_cache.Set(_scheduleKey, data, cacheEntryOptions);
			}
		}

		private void SaveLeagues(List<League> data)
		{
			if (!_cache.TryGetValue(_leagueKey, out _))
			{
				MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(8));
				_cache.Set(_leagueKey, data, cacheEntryOptions);
			}
		}

		private void SaveTeams(List<League> data)
		{
			List<string> teamList = new List<string>();

			foreach (var team in data)
			{
				teamList.AddRange(team.Teams);
			}

			if (!_cache.TryGetValue(_teamKey, out _))
			{
				MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(8));
				_cache.Set(_teamKey, teamList, cacheEntryOptions);
			}
		}
	}
}
