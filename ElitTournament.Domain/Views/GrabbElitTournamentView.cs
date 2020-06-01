using ElitTournament.Domain.Entities;
using System.Collections.Generic;

namespace ElitTournament.Domain.Views
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
