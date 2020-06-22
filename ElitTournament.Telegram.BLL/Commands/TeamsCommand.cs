using ElitTournament.DAL.Entities;
using ElitTournament.Core.Helpers.Interfaces;
using ElitTournament.Telegram.BLL.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElitTournament.DAL.Repositories.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ElitTournament.Telegram.BLL.Commands
{
    public class TeamsCommand : Command
    {
        private readonly ILeagueRepository _leagueRepository;

        public TeamsCommand(ILeagueRepository leagueRepository, int lastVersion) : base(lastVersion)
        {
            _leagueRepository = leagueRepository;
        }

        public async override Task<bool> Contains(string command)
        {
            IEnumerable<League> teams = await _leagueRepository.GetAll(version);
            IEnumerable<string> teamNames = teams.Select(p => p.Name);
            return teamNames.Contains(command);
        }

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = await GetMenu(message.Text);
            await client.SendTextMessageAsync(message.Chat.Id, MessageConstant.CHOOSE_TEAM, ParseMode.Html, false, false, 0, replyKeyboardMarkup);
        }

        private async Task<ReplyKeyboardMarkup> GetMenu(string command)
        {
            IEnumerable<League> leagues = await _leagueRepository.GetAll(version);
            League currentLeague = leagues.FirstOrDefault(p => p.Name == command);
            List<List<KeyboardButton>> buttons = CreateButtons(currentLeague, command);
            ReplyKeyboardMarkup menu = new ReplyKeyboardMarkup(buttons);
            // menu.Keyboard = buttons;

            return menu;
        }

        private List<List<KeyboardButton>> CreateButtons(League currentLeague, string command)
        {
            List<List<KeyboardButton>> keyBoard = new List<List<KeyboardButton>>();

            int teamsCount = currentLeague.Teams.Count();
            bool isEven = teamsCount % 2 == 0 ? true : false;

            for (int i = 0; i < teamsCount; i++)
            {
                List<KeyboardButton> teams = new List<KeyboardButton>();

                if (isEven)
                {
                    teams.Add(new KeyboardButton(currentLeague.Teams[i].Name));
                    i++;
                    teams.Add(new KeyboardButton(currentLeague.Teams[i].Name));
                }
                else
                {
                    teams.Add(new KeyboardButton(currentLeague.Teams[i].Name));
                    i++;
                    if (i != currentLeague.Teams.Count)
                    {
                        teams.Add(new KeyboardButton(currentLeague.Teams[i].Name));
                    }
                }

                keyBoard.Add(teams);
            }
            
            //keyBoard.Insert(0,new List<KeyboardButton> { new KeyboardButton($"Турирная таблица {command}") });
            keyBoard.Add(new List<KeyboardButton> { new KeyboardButton("Назад") });
            return keyBoard;
        }
    }
}
