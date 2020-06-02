using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ElitTournament.Viber.Core.View
{
	public class GetAccountInfoResponse : ApiResponseBase, IAccountInfo
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("uri")]
		public string Uri { get; set; }

		[JsonProperty("icon")]
		public string Icon { get; set; }

		[JsonProperty("background")]
		public string Background { get; set; }

		[JsonProperty("category")]
		public string Category { get; set; }

		[JsonProperty("subcategory")]
		public string Subcategory { get; set; }

		[JsonProperty("event_types")]
		public string[] EventTypes { get; set; }

		[JsonProperty("location")]
		public Location Location { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("webhook")]
		public string Webhook { get; set; }

		[JsonProperty("subscribers_count")]
		public long SubscribersCount { get; set; }

		[JsonProperty("members")]
		public ICollection<ChatMember> Members { get; set; }
	}
}
