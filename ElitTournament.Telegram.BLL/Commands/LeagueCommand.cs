using ElitTournament.DAL.Entities;
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
    public class LeagueCommand : Command
    {
        private readonly ILeagueRepository _leagueRepository;

        public LeagueCommand(ILeagueRepository leagueRepository, int lastVersion) : base(lastVersion)
        {
            _leagueRepository = leagueRepository;
        }
        
        public async override Task Execute(Message message, ITelegramBotClient client)
        {
            string text = MessageConstant.CHOOSE_LEAGUE;
            if (message.Text == MessageConstant.DEVELOP)
            {
                text = "В разработке:\n1. Бот будет сам уведомлять когда игра\n\nСвязь с разработчиком\n0955923228";
            }

            ReplyKeyboardMarkup keyboard = await GetMenu();
            
            await client.SendTextMessageAsync(message.Chat.Id, text, ParseMode.Html, false, false, 0, keyboard);
        }

        private async Task<ReplyKeyboardMarkup> GetMenu()
        {
            IEnumerable<League> leagues = await _leagueRepository.GetAll(version);
            
            var menu = new ReplyKeyboardMarkup();
            List<List<KeyboardButton>> list = new List<List<KeyboardButton>>();

            if (leagues != null)
            {
                List<League> leaguesList = leagues.ToList();
                int leaguesCount = leaguesList.Count();
                bool isEven = leaguesCount % 2 == 0 ? true : false;
                
                // TODO: refactor this
                for (int i = 0; i < leaguesCount; i++)
                {
                    List<KeyboardButton> test = new List<KeyboardButton>();
                    if (isEven)
                    {
                        test.Add(new KeyboardButton(leaguesList[i].Name));
                        i++;
                        test.Add(new KeyboardButton(leaguesList[i].Name));
                    }
                    else
                    {
                        test.Add(new KeyboardButton(leaguesList[i].Name));
                        i++;
                        if (i != leaguesCount)
                        {
                            test.Add(new KeyboardButton(leaguesList[i].Name));
                        }
                    }

                    list.Add(test);
                }
            }

            list.Add(new List<KeyboardButton> { new KeyboardButton(MessageConstant.DEVELOP) });
            menu.Keyboard = list;

            return menu;
        }
    }
}
