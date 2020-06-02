using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.View
{
	public class SetWebhookResponse : ApiResponseBase
	{
		/// <summary>
		/// List of event types you will receive a callback for. Should return the same values sent in the request.
		/// </summary>
		[JsonProperty("event_types")]
		public string[] EventTypes { get; set; }
	}
}
