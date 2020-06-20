using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElitTournament.DAL.Entities
{
	public class DataVersion
	{
		[Key]
		public int Version { get; set; }

		public IEnumerable<Schedule> Schedules { get; set; }

		public IEnumerable<League> Leagues { get; set; }
	}
}
