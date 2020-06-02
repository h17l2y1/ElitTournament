using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models
{
	public class ChatMember : UserBase
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("role")]
		public ChatMemberRoleType Role { get; set; }
	}
}
