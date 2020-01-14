using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ElitTournament.Domain.Providers.Interfaces
{
    public interface IBotProvider
    {
        Task Update(Update update);
        Task InitializeClient();
    }
}
