using ElitTournament.DAL.Config;
using ElitTournament.DAL.Entities;
using ElitTournament.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElitTournament.DAL.Repositories
{
	public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
	{
		public ScheduleRepository(ApplicationContext context) : base(context)
		{
		}

		public async override Task<IEnumerable<Schedule>> GetAll()
		{
			IEnumerable<Schedule> schedule = await _dbSet.Include(x => x.Games).AsNoTracking().ToListAsync();
			return schedule;
		}

		public async Task<string> FindGame(string teamName)
		{
			ICollection<Schedule> schedule = await _dbSet.AsNoTracking().ToListAsync();

			List<string> list = new List<string>();
			string teamWithSpace = teamName.Replace("-", " ").ToUpper();

			foreach (var place in schedule)
			{
				foreach (var game in place.Games)
				{
					string gameString = game.Match.Replace("-", " ").ToUpper();
					bool teamIsExist = Regex.IsMatch(gameString, $"\\b{teamWithSpace}\\b");
					if (teamIsExist)
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

			return null;
		}
	}
}
