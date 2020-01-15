using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ElitTournament.Domain.Commands
{
    public class ScheduleCommand : Command
    {
        private List<League> _leagues;
        private ICacheHelper _cacheHelper;
        public ScheduleCommand(ICacheHelper cacheHelper) :base("")
        {
            _cacheHelper = cacheHelper;
            _leagues = new List<League>();
            for (int i = 0; i < 10; i++)
            {
                var leageNum = i + 1;
                var league = new League($"Leage {leageNum}");
                for (int j = 0; j < 10; j++)
                {
                    var teamNum = j + 1;
                    league.Teams.Add($"Team {teamNum}");
                }
                _leagues.Add(league);
            }
        }
        public override bool Contains(string command)
        {
            foreach (var item in _leagues)
            {
               var team =  item.Teams.FirstOrDefault(p => p.Contains(command));
                if(team != null)
                {
                    return true;
                }
            }

            return false;
        }
        public async override void Execute(Message message, TelegramBotClient client)
        {
           // _cacheHelper.
        }
    }
}
