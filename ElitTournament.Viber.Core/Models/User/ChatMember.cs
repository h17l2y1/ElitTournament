using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models.User
{
	public class ChatMember : UserBase
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("role")]
		public ChatMemberRole Role { get; set; }
	}
}