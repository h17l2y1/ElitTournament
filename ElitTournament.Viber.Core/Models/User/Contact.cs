using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models.User
{
	public class Contact
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("phone_number")]
		public string TN { get; set; }
	}
}
