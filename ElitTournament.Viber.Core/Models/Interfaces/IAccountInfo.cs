using System.Collections.Generic;

namespace ElitTournament.Viber.Core.Models.Interfaces
{
	public interface IAccountInfo
	{
		string Id { get; }

		string Name { get; }

		string Uri { get; }

		string Icon { get; }

		string Background { get; }

		string Category { get; }

		string Subcategory { get; }

		string[] EventTypes { get; }

		Location Location { get; }

		string Country { get; }

		string Webhook { get; }

		long SubscribersCount { get; }

		ICollection<ChatMember> Members { get; }
	}
}
