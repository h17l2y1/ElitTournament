using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Commands
{
    public abstract class Command
    {
        public async virtual Task<bool> Contains(string command)
        {
            return true;
        }

        public abstract Task Execute(Callback callback, IViberBotClient client);
    }
}
