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

		public ScheduleCommand(ICacheHelper cacheHelper) : base("")
		{
			_cacheHelper = cacheHelper;
			_leagues = _cacheHelper.GetLeagues();
		}

		public override bool Contains(string text)
		{
			bool isExistTeam = false;
			if (_leagues != null)
			{
				List<string> teams = _cacheHelper.GetTeams();

				string teamWithSpace = text.Replace("-", " ").ToUpper();
				string teamWithHyphen = text.Replace(" ", "-").ToUpper();

				string spaceTeam = teams.FirstOrDefault(x => x == teamWithSpace);
				string hyphenTeam = teams.FirstOrDefault(x => x == teamWithHyphen);

				if (spaceTeam != null || hyphenTeam != null)
				{
					isExistTeam = true;
				}
			}
			return isExistTeam;
		}

		public async override void Execute(Message message, TelegramBotClient client)
		{
			List<string> schedule = _cacheHelper.FindGame(message.Text);
			long chatId = message.Chat.Id;

			if (schedule == null)
			{
				string notFound = $"Не получилось получить расписание игр, поищите свою игру тут http://elitturnir.info/raspisanie/";
				await client.SendTextMessageAsync(chatId, notFound);
				return;
			}

			if (schedule.Count == 0)
			{
				string notFound = $"Игры команды \"{message.Text}\" не найдено";
				await client.SendTextMessageAsync(chatId, notFound);
				return;
			}

			var result = String.Join(", ", schedule.ToArray());

			await client.SendTextMessageAsync(chatId, result);
		}
	}
}
