﻿using ElitTournament.DAL.Config;
using ElitTournament.DAL.Enities;
using ElitTournament.DAL.Repositories.Interfaces;

namespace ElitTournament.DAL.Repositories
{
	public class GameRepository : BaseRepository<Game>, IGameRepository
	{
		public GameRepository(ApplicationContext context) : base(context)
		{
		}
	}
}
