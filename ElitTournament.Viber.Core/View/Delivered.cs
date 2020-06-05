using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.View
{
	public class Delivered
	{
		[JsonProperty("event")]
		public string Event { get; set; }

		[JsonProperty("timestamp")]
		public long Timestamp { get; set; }

		[JsonProperty("message_token")]
		public long MessageToken { get; set; }

		[JsonProperty("user_id")]
		public string UserId { get; set; }
	}
}
