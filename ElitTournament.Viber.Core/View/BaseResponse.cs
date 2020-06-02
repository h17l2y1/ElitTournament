using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.View
{
	public abstract class ApiResponseBase
	{
		[JsonProperty("status")]
		public ErrorCode Status { get; set; }

		[JsonProperty("status_message")]
		public string StatusMessage { get; set; }
	}
}
