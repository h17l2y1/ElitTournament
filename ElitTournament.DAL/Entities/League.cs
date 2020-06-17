﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElitTournament.DAL.Entities
{
	public class League : BaseEntity
	{
		public string Name { get; set; }
		public List<Team> Teams { get; set; }

		public League()
		{
			Teams = new List<Team>();
		}

		public League(string name)
		{
			Name = name;
			Teams = new List<Team>();
		}

		[ForeignKey("DataVersionId")]
		public int DataVersionId { get; set; }

		[NotMapped]
		public virtual DataVersion DataVersion { get; set; }
	}
}