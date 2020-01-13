using ElitTournament.Domain.Helpers.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

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

		// Object it's temporary
		// have not time to implement it
		public void Save(Object data)
		{
			Object deck = data;
			if (!_cache.TryGetValue(_key, out data))
			{
				data = deck;
				MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
				_cache.Set(_key, data, cacheEntryOptions);
			}
		}

		public Object Get()
		{
			Object data = _cache.Get<Object>(_key);
			if (data == null)
			{
				//throw new NotFoundException();
			}
			return data;
		}
	}
}
