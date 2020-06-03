using ElitTournament.Core.Entities;
using System.Collections.Generic;

namespace ElitTournament.Core.Views
{
	public class GrabbElitTournamentView
	{
		public List<Schedule> Schedule { get; set; }
		public List<League> Leagues { get; set; }

		public GrabbElitTournamentView(List<Schedule> schedule, List<League> leagues)
		{
			Schedule = schedule;
			Leagues = leagues;
		}
	}
}
