using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models
{
	public class WebHookViber
	{
        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("event_types")]
        public string[] EventTypes { get; set; }

        [JsonProperty("send_name")]
        public bool SendName { get; set; }

        [JsonProperty("send_photo")]
        public bool SendPhoto { get; set; }
    }
}
