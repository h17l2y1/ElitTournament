using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models
{
	public abstract class MessageBase
	{
		protected MessageBase(MessageType type)
		{
			Type = type;
		}

		[JsonProperty("receiver")]
		public string Receiver { get; set; }

		[JsonProperty("type")]
		public MessageType Type { get; }

		[JsonProperty("sender")]
		public UserBase Sender { get; set; }

		[JsonProperty("tracking_data")]
		public string TrackingData { get; set; }

		[JsonProperty("min_api_version")]
		public double? MinApiVersion { get; set; }
	}
}
