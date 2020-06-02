using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.View
{
	public class SendMessageResponse : ApiResponseBase
	{
		[JsonProperty("message_token")]
		public long MessageToken { get; set; }
	}
}
