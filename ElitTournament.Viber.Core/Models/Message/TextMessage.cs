using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models.Message
{
	public class TextMessage : MessageBase
	{
		[JsonProperty("text")]
		public string Text { get; set; }

		public TextMessage(string receiverId, string text) : base(MessageType.Text, receiverId)
		{
			Text = text;
		}
	}
}
