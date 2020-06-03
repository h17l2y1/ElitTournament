using System.Collections.Generic;

namespace ElitTournament.Core.Entities
{
	public class Schedule
	{
		public string Place { get; set; }
		public List<string> Games { get; set; }

		public Schedule(string place)
		{
			Place = place;
			Games = new List<string>();
		}
	}
}
