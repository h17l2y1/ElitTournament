using System.Collections.Generic;

namespace ElitTournament.Domain.Entities
{
	public class League
	{
		public string Name { get; set; }
		public List<string> Teams { get; set; }

		public League()
		{
			Teams = new List<string>();
		}
	}
}
