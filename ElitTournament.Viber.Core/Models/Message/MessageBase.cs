using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models.Message
{
	public abstract class MessageBase
	{
		protected MessageBase(MessageType type)
		{
			Type = type;
			MinApiVersion = 1;
		}

		[JsonProperty("receiver")]
		public string Receiver { get; set; }

		[JsonProperty("min_api_version")]
		public double? MinApiVersion { get; set; }

		[JsonProperty("sender")]
		public UserBase Sender { get; set; }

		[JsonProperty("tracking_data")]
		public string TrackingData { get; set; }

		[JsonProperty("type")]
		public MessageType Type { get; }

	}
}
