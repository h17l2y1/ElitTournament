using System.Collections.Generic;
using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models.Message
{
	public class BroadcastMessage : MessageBase
	{
		public BroadcastMessage(string receiverId) : base(MessageType.Text, receiverId)
		{
		}

		[JsonProperty("text")]
		public string Text { get; set; }
		
		[JsonProperty("broadcast_list")]
		public IEnumerable<string> BroadcastList { get; set; }
		
		// [JsonProperty("rich_media")]
		// public RichMedia RichMedia { get; set; }
	}
}