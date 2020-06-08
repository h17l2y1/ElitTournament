using ElitTournament.Viber.Core.Enums;
using ElitTournament.Viber.Core.Models.Message;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models
{
	public class Callback
	{
		[JsonProperty("event")]
		public EventType Event { get; set; }

		[JsonProperty("message_token")]
		public long MessageToken { get; set; }

		[JsonProperty("timestamp")]
		public long Timestamp { get; set; }

		[JsonProperty("user_id")]
		public string UserId { get; set; }

		[JsonProperty("user")]
		public UserModel User { get; set; }

		[JsonProperty("subscribed")]
		public bool? Subscribed { get; set; }

		[JsonProperty("context")]
		public string Context { get; set; }

		[JsonProperty("desc")]
		public string Description { get; set; }

		[JsonProperty("sender")]
		public UserModel Sender { get; set; }

		[JsonProperty("message")]
		public TextMessage Message { get; set; }
	}
}
