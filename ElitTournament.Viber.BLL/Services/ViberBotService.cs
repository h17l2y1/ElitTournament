using ElitTournament.Viber.BLL.Services.Interfaces;
using ElitTournament.Viber.BLL.View;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.View;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Threading.Tasks;

namespace ElitTournament.Viber.BLL.Services
{
	public class ViberBotService : IViberBotService
	{
		private readonly IViberBotClient _viberBotClient;
		private readonly string _webHook;
		private readonly string _viberUrl;

		private readonly string tkn;

		public ViberBotService(IConfiguration configuration, IViberBotClient viberBotClient)
		{
			_viberBotClient = viberBotClient;
			_webHook = configuration["WebHook"];
			_viberUrl = "/api/viber/update";

			tkn = configuration["Token"];
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
			string webHook = $"{_webHook}{_viberUrl}";
			var client = new RestClient(webHook);
			var request = new RestRequest(Method.POST);
			request.RequestFormat = DataFormat.Json;
			request.AddHeader("X-Viber-Auth-Token", tkn);

			var res = new SendViberMessageView();
			res.Receiver = view.Sender?.Id;
			res.Text = $"Выберите лигу";
			res.Type = "text";
			res.Keyboard = new ViberKeyboard();
			res.Keyboard.DefaultHeight = true;
			res.Keyboard.Type = "keyboard";
			//res.Keyboard.Buttons = new List<Button>();
			//res.Keyboard.Buttons.Add(new Button { ActionBody = "reply", ActionType = "reply", Text = "Call me", TextSize = "regular" });
			request.AddJsonBody(res);
			IRestResponse response = client.Execute(request);
		}
	}
}
