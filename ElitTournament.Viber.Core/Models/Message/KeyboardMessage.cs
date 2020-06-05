using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models.Message
{
	public class KeyboardMessage : MessageBase
	{
		public KeyboardMessage()
			: base(MessageType.Text)
		{
		}

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("keyboard")]
		public Keyboard Keyboard { get; set; }
	}
}
