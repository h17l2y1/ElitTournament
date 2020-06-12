using ElitTournament.DAL.Config;
using ElitTournament.DAL.Entities;
using ElitTournament.DAL.Repositories.Interfaces;

namespace ElitTournament.DAL.Repositories
{
	public class TeamRepository : BaseRepository<Team>, ITeamRepository
	{
		public TeamRepository(ApplicationContext context) : base(context)
		{
		}
	}
}
