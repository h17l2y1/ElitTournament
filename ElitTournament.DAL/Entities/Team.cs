using System.ComponentModel.DataAnnotations.Schema;

namespace ElitTournament.DAL.Entities
{
	public class Team : BaseEntity
	{
		public Team(string name)
		{
			Name = name;
		}

		public Team()
		{
		}

		public int Position { get; set; }

		public string Name { get; set; }

		public int Played { get; set; }

		public int Won { get; set; }

		public int Drawn { get; set; }

		public int Lost { get; set; }

		public int Goals { get; set; }

		public int GoalDifference { get; set; }

		public int Points { get; set; }

		[ForeignKey("LeagueId")]
		public int LeagueId { get; set; }

		[NotMapped]
		public virtual League League { get; set; }
	}
}
