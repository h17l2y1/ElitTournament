using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models.Message
{
	public class FileMessage : MessageBase
	{
		public FileMessage(string receiverId) : base(MessageType.File, receiverId)
		{
		}
		
		[JsonProperty("media")]
		public string Media { get; set; }
		
		[JsonProperty("size")]
		public int Size { get; set; }
		
		[JsonProperty("file_name")]
		public string FileName { get; set; }
	}
}