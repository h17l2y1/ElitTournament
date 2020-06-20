using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Telegram.BLL.Commands
{
    public abstract class Command
    {
        protected int version;

        public Command(int lastVersion)
        {
            version = lastVersion;
        }
        
        public virtual async Task<bool> Contains(string command)
        {
            return true;
        }

        public abstract Task Execute(Message message, ITelegramBotClient client);

    }
}
