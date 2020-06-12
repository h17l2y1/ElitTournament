using ElitTournament.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElitTournament.DAL.Enities
{
	public class Game : BaseEntity
	{
		public Game(string match)
		{
			Match = match;
		}

		public Game()
		{
		}

		public string Match { get; set; }

		[ForeignKey("ScheduleId")]
		public int ScheduleId { get; set; }

		[NotMapped]
		public virtual Schedule Schedule { get; set; }
	}
}
