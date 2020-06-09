using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Telegram.BLL.Commands
{
    public abstract class Command
    {
        public virtual bool Contains(string command)
        {
            return true;
        }

        public abstract void Execute(Message message, ITelegramBotClient client);

    }
}
