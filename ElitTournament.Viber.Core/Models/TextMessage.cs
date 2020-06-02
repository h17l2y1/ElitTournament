using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models
{
	public class TextMessage : MessageBase
	{
		public TextMessage() : base(MessageType.Text)
		{
		}

		[JsonProperty("text")]
		public string Text { get; set; }
	}
}
