using System;
using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models.Message
{
	public class PictureMessage : MessageBase
	{
		public PictureMessage(string receiverId) : base(MessageType.Picture, receiverId)
		{
			Text = String.Empty;
		}
		
		[JsonProperty("text")]
		public string Text { get; set; }
		
		[JsonProperty("media")]
		public string Media { get; set; }
		
		[JsonProperty("thumbnail")]
		public string Thumbnail { get; set; }
	}
}