using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Domain.Commands
{
    public abstract class Command
    {
        public  string Name { get; set; }
        public string Text { get; set; }

        public Command(string commandName)
        {
            Name = commandName;
        }

        public abstract void Execute(Message message, TelegramBotClient client);

        public virtual bool Contains(string command)
        {
            return command.ToLower().Contains(Name.ToLower());
        }
    }
}
