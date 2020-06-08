using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;

namespace ElitTournament.Viber.BLL.Commands
{
    public abstract class Command
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public virtual bool Contains(string command)
        {
            return command.ToLower().Contains(Name.ToLower());
        }

        public abstract void Execute(Callback callback, IViberBotClient client);

    }
}
