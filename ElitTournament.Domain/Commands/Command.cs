using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Domain.Commands
{
    public abstract class Command
    {
        public readonly string _name;
        public string Text { get; set; }

        public Command(string commandName)
        {
            _name = commandName;
        }

        public abstract void Execute(Message message, TelegramBotClient client);

        public bool Contains(string command)
        {
            return command.ToLower().Contains(this._name.ToLower());
        }
    }
}
