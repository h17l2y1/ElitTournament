using ElitTournament.DAL.Enities;
using System.Collections.Generic;

namespace ElitTournament.DAL.Entities
{
	public class Schedule : BaseEntity
	{
		public string Place { get; set; }
		public List<Game> Games { get; set; }

		public Schedule(string place)
		{
			Place = place;
			Games = new List<Game>();
		}
	}
}
