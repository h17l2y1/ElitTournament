using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Domain.Commands
{
    public class TeamsCommand : Command
    {
        public TeamsCommand() : base("расписание")
        {
           
        }
        public override void Execute(Message message, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }
}
