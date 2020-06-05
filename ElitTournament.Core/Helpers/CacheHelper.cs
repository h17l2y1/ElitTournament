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
		private readonly string _ScheduleKey = "1908f0292df1";
		private readonly string _LeagueKey = "14da59f1006a";
		private readonly string _TeamKey = "77fg21v0984c";

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
		}

		public List<League> GetLeagues()
		{
			List<League> leagues = _cache.Get<List<League>>(_LeagueKey);
			if (leagues == null)
			{
				return null;
			}

			return leagues;
		}

		public List<Schedule> GetSchedule()
		{
			List<Schedule> schedule = _cache.Get<List<Schedule>>(_ScheduleKey);
			if (schedule == null)
			{
				return null;
			}

			return schedule;
		}

		public List<string> GetTeams()
		{
			List<string> teams = _cache.Get<List<string>>(_TeamKey);
			if (teams != null)
			{
				return teams;
			}
			return null;
		}

		public List<string> FindGame(string teamName)
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

				return list;
			}

			return null;
		}

		private void Clear()
		{
			_cache.Remove(_ScheduleKey);
			_cache.Remove(_LeagueKey);
			_cache.Remove(_TeamKey);
		}

		private void SaveSchedule(List<Schedule> data)
		{
			List<Schedule> deck = data;
			if (!_cache.TryGetValue(_ScheduleKey, out data))
			{
				data = deck;
				MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(8));
				_cache.Set(_ScheduleKey, data, cacheEntryOptions);
			}
		}

		private void SaveLeagues(List<League> data)
		{
			if (!_cache.TryGetValue(_LeagueKey, out _))
			{
				MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(8));
				_cache.Set(_LeagueKey, data, cacheEntryOptions);
			}
		}

		private void SaveTeams(List<League> data)
		{
			List<string> teamList = new List<string>();

			foreach (var team in data)
			{
				teamList.AddRange(team.Teams);
			}

			if (!_cache.TryGetValue(_TeamKey, out _))
			{
				MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(8));
				_cache.Set(_TeamKey, teamList, cacheEntryOptions);
			}
		}
	}
}
