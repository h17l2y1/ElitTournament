using ElitTournament.Viber.Core.Models;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.View
{
	public class GetUserDetailsResponse : ApiResponseBase
	{
		[JsonProperty("message_token")]
		public long MessageToken { get; set; }

		[JsonProperty("user")]
		public UserDetails User { get; set; }
	}
}
