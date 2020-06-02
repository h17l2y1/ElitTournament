using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers.Interfaces;
using ElitTournament.Domain.Views;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.View;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers
{
    public class ViberProvider : IViberProvider
    {
        private readonly string _viberToken;
        private readonly string _viberWebHook;
        private readonly IViberBotClient _viberBotClient;

        public ViberProvider(IConfiguration configuration, IViberBotClient viberBotClient)
        {
            _viberToken = configuration["Viber"];
            _viberWebHook = configuration["ViberWebHook"];

            _viberBotClient = viberBotClient;
            //_viberBotClient = new ViberBotClient(_viberWebHook);
        }

        public async Task<SetWebhookResponse> SetWebHookToken()
        {
            var viberClient = new ViberBotClient(_viberToken);
            SetWebhookResponse res = await viberClient.SetWebhookAsync(_viberWebHook);       
            return res;
        }

        public async Task RemoveWebHookToken()
        {
            await _viberBotClient.RemoveWebhookAsync();
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
            var client = new RestClient("https://21bbf222f958.ngrok.io/api/viber/update");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("X-Viber-Auth-Token", _viberToken);

            var res = new SendViberMessageView();
            res.Receiver = view.Sender?.Id;
            res.Text = $"Выберите лигу";
            res.Type = "text";
            res.Keyboard = new ViberKeyboard();
            res.Keyboard.DefaultHeight = true;
            res.Keyboard.Type = "keyboard";
            res.Keyboard.Buttons = new List<Button>();
            res.Keyboard.Buttons.Add(new Button { ActionBody = "reply", ActionType = "reply", Text = "Call me", TextSize = "regular" });
            request.AddJsonBody(res);
            IRestResponse response = client.Execute(request);
        }

    }

}
