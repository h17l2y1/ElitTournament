using ElitTournament.Viber.BLL.Helpers.Interfaces;
using ElitTournament.Viber.BLL.Services.Interfaces;
using ElitTournament.Viber.BLL.View;
using ElitTournament.Viber.Core.Enums;
using ElitTournament.Viber.Core.Model;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using ElitTournament.Viber.Core.View;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Services
{
	public class ViberBotService : IViberBotService
	{
		private readonly IViberHelper _viberHelper;
		private readonly IViberBotClient _viberBotClient;
		//private readonly ICacheHelper _cacheHelper;
		private readonly string _webHook;
		private readonly string _viberUrl;

		public ViberBotService(IConfiguration configuration, IViberBotClient viberBotClient, /*ICacheHelper cacheHelper,*/ IViberHelper viberHelper)
		{
			_viberHelper = viberHelper;
			_viberBotClient = viberBotClient;
			//_cacheHelper = cacheHelper;
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

			if (view.Event == EventType.Message.ToString().ToLower())
			{
				await _viberHelper.SendTextMessage(view);
				return;
			}
		}

	}

}
