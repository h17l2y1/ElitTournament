using ElitTournament.Viber.Core.Models.Interfaces;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models
{
	public class UserBase : IUserBase
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("avatar")]
		public string Avatar { get; set; }
	}
}
