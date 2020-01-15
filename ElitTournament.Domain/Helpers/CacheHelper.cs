using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace ElitTournament.Domain.Helpers
{
	public class CacheHelper : ICacheHelper
	{
		private IMemoryCache _cache;
		private readonly string _FindGameKey = "1908f0292df1";
		private readonly string _LeagueKey = "14da59f1006a";

		public CacheHelper(IMemoryCache cache)
		{
			_cache = cache;
		}

		public void Update(List<Schedule> schedule, List<League> league)
		{
			Clear();
			SaveSchedule(schedule);
			SaveLeagues(league);
		}

		private void Clear()
		{
			_cache.Remove(_FindGameKey);
			_cache.Remove(_LeagueKey);
		}

		private void SaveSchedule(List<Schedule> data)
		{
			List<Schedule> deck = data;
			if (!_cache.TryGetValue(_FindGameKey, out data))
			{
				data = deck;
				MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(8));
				_cache.Set(_FindGameKey, data, cacheEntryOptions);
			}
		}

		private void SaveLeagues(List<League> data)
		{
			List<League> deck = data;
			if (!_cache.TryGetValue(_FindGameKey, out data))
			{
				data = deck;
				MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(8));
				_cache.Set(_FindGameKey, data, cacheEntryOptions);
			}
		}

		public string FindGame(string teamName)
		{
			List<Schedule> schedule = _cache.Get<List<Schedule>>(_FindGameKey);
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
