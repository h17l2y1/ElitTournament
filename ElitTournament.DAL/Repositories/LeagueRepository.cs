using ElitTournament.DAL.Entities;
using ElitTournament.DAL.Config;
using ElitTournament.DAL.Repositories.Interfaces;

namespace ElitTournament.DAL.Repositories
{
	public class LeagueRepository : BaseRepository<League>, ILeagueRepository
	{
		public LeagueRepository(ApplicationContext context) : base(context)
		{
		}
	}
}
