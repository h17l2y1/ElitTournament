using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models.Message
{
	public class KeyboardMessage : MessageBase
	{
		public KeyboardMessage(string receiverId, string text) : base(MessageType.Text, receiverId)
		{
			Text = text;
		}

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("keyboard")]
		public Keyboard Keyboard { get; set; }
	}
}
