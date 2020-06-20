using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElitTournament.DAL.Entities
{
	public class League : BaseEntity
	{
		public League(string name)
		{
			Name = name;
			Teams = new List<Team>();
		}
		
		public string Name { get; set; }
		public List<Team> Teams { get; set; }

		public string Link { get; set; }

		public int Size { get; set; }
		
		[ForeignKey("DataVersionId")]
		public int DataVersionId { get; set; }

		[NotMapped]
		public virtual DataVersion DataVersion { get; set; }
	}
}
