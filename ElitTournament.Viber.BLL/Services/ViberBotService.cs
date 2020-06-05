using ElitTournament.Core.Services.Interfaces;
using ElitTournament.Viber.BLL.Services.Interfaces;
using ElitTournament.Viber.BLL.View;
using ElitTournament.Viber.Core.Enums;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.View;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Services
{
	public class ViberBotService : IViberBotService
	{
		private readonly IViberBotClient _viberBotClient;
		private readonly IScheduleService _scheduleService;
		private readonly string _webHook;
		private readonly string _viberUrl;

		public ViberBotService(IConfiguration configuration, IViberBotClient viberBotClient, IScheduleService scheduleService)
		{
			_viberBotClient = viberBotClient;
			_scheduleService = scheduleService;
			_webHook = configuration["WebHook"];
			_viberUrl = "/api/viber/update";
		}

		public async Task<SetWebhookResponse> SetWebHookAsync()
		{
			string webHook = $"{_webHook}{_viberUrl}";
			SetWebhookResponse res = await _viberBotClient.SetWebhookAsync(webHook);
			return res;
		}

		public async Task<IAccountInfo> GetAccountInfo()
		{
			IAccountInfo res = await _viberBotClient.GetAccountInfoAsync();
			return res;
		}

		public async Task<long> SendTextMessage(string text)
		{
			TextMessage message = new TextMessage { Text = text };

			long res = await _viberBotClient.SendTextMessageAsync(message);
			return res;
		}

		public async Task Update(RootObject view)
		{
			if (view.Event == EventType.Webhook.ToString().ToLower())
			{
				return;
			}

			if (view.Event == EventType.Message.ToString().ToLower())
			{
				var x = _scheduleService.FindGame(view.Message.Text);

				var result = await _viberBotClient.SendTextMessageAsync(new TextMessage
				{
					Receiver = view.Sender.Id,
					Sender = new UserBase
					{
						Name = "Элит Турнир",
						Avatar = "https://media-direct.cdn.viber.com/pg_download?pgtp=icons&dlid=0-04-01-f41cdc768c7d7cedd43ea09bfac31374299fde787fb57b0ff50efa1143fa87fd&fltp=jpg&imsz=0000"
					},
					Text = x,
					MinApiVersion = 1,
					TrackingData = "tracking data"
				});
				return;
			}

			if (view.Event == EventType.Delivered.ToString().ToLower())
			{
				var c = new Delivered
				{
					Event = EventType.Seen.ToString().ToLower(),
					Timestamp = view.Timestamp,
					MessageToken = view.Message_token,
					UserId = view.User_Id,
				};

				return;
			}

		}
	}


}
