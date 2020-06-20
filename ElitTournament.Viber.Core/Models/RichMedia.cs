using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models
{
	public class RichMedia
	{
		[JsonProperty("Type")]
		private const string Type = "rich_media";

		[JsonProperty("BgColor")]
		public string BackgroundColor { get; set; }
		
		[JsonProperty("Buttons")]
		public ICollection<Button> Buttons { get; set; }
	}
}