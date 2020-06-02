using ElitTournament.Viber.Core.Enums;
using ElitTournament.Viber.Core.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Viber.Core.Models.Interfaces
{
	public interface IViberBotClient
	{
		Task<SetWebhookResponse> SetWebhookAsync(string url, ICollection<EventType> eventTypes = null, bool sendName = true, bool sendPhoto = true);

		Task RemoveWebhookAsync();

		Task<IAccountInfo> GetAccountInfoAsync();

		Task<long> SendTextMessageAsync(TextMessage message);
	}
}
