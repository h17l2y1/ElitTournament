using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models
{
	public class Location
	{
		[JsonProperty("lon")]
		public double Lon { get; set; }

		[JsonProperty("lat")]
		public double Lat { get; set; }
	}
}
