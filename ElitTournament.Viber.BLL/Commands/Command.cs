using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using System.Threading.Tasks;
using ElitTournament.DAL.Repositories.Interfaces;

namespace ElitTournament.Viber.BLL.Commands
{
    public abstract class Command
    {
        protected int version;

        public Command(int lastVersion)
        {
            version = lastVersion;
        }
        
        public async virtual Task<bool> Contains(string command)
        {
            return true;
        }

        public abstract Task Execute(Callback callback, IViberBotClient client);
    }
}
