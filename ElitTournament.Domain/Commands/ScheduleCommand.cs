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

		public override bool Contains(string command)
		{
			if (_leagues != null)
			{
				foreach (var item in _leagues)
				{
					var team = item.Teams.FirstOrDefault(p => p == command.ToUpper());
					if (team != null)
					{
						return true;
					}
				}
			}

			return false;
		}

		public async override void Execute(Message message, TelegramBotClient client)
		{
			List<string> schedule = _cacheHelper.FindGame(message.Text);
			long chatId = message.Chat.Id;

			if (schedule.Count == 0)
			{
				await client.SendTextMessageAsync(chatId, "Игры не найдено или неправильно введено название команды");
				return;
			}
			var result = String.Join(", ", schedule.ToArray());

			await client.SendTextMessageAsync(chatId, result);
		}
	}
}
