using ElitTournament.Domain.Views;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.View;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers.Interfaces
{
    public interface IViberProvider
    {
        Task<SetWebhookResponse> SetWebHookToken();

        Task RemoveWebHookToken();

        Task<IAccountInfo> GetAccountInfo();

        Task<long> SendTextMessage(string text);

        Task Update(RootObject res);
    }
}
