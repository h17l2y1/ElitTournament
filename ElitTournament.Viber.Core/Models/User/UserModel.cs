using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models
{
	public class UserModel : UserBase
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("language")]
		public string Language { get; set; }

		[JsonProperty("api_version")]
		public double ApiVersion { get; set; }
	}
}
