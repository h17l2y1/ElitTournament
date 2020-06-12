using ElitTournament.DAL.Entities;
using System.Collections.Generic;

namespace ElitTournament.Core.Views
{
	public class GrabbElitTournamentView
	{
		public IEnumerable<Schedule> Schedule { get; set; }
		public IEnumerable<League> Leagues { get; set; }

		public GrabbElitTournamentView(IEnumerable<Schedule> schedule, IEnumerable<League> leagues)
		{
			Schedule = schedule;
			Leagues = leagues;
		}
	}
}
