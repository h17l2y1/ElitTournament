using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElitTournament.DAL.Entities
{
	public class Schedule : BaseEntity
	{
		public Schedule(string place)
		{
			Place = place;
			Games = new List<Game>();
		}

		public string Place { get; set; }

		public List<Game> Games { get; set; }

		[ForeignKey("DataVersionId")]
		public int DataVersionId { get; set; }

		[NotMapped]
		public virtual DataVersion DataVersion { get; set; }
	}
}
