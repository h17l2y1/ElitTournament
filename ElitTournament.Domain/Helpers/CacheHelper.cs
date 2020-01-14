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
		private readonly string _key = "1908f029-ee1a-447d-ba23-14da59f1006a";

		public CacheHelper(IMemoryCache cache)
		{
			_cache = cache;
		}

		public void Clear()
		{
			_cache.Remove(_key);
		}

		public void Update(List<Schedule> data)
		{
			Clear();
			SaveSchedule(data);
		}

		public void SaveSchedule(List<Schedule> data)
		{
			List<Schedule> deck = data;
			if (!_cache.TryGetValue(_key, out data))
			{
				data = deck;
				MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
				_cache.Set(_key, data, cacheEntryOptions);
			}
		}

		public List<Schedule> Get()
		{
			List<Schedule> data = _cache.Get<List<Schedule>>(_key);
			if (data == null)
			{
				//throw new NotFoundException();
			}
			return data;
		}
	}
}
