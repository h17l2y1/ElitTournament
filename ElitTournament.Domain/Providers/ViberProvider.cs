using ElitTournament.Domain.Helpers.Interfaces;
using ElitTournament.Domain.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers
{
    public class ViberProvider : IViberProvider
    {
        private readonly string _viberToken;
        private readonly string _viberUrl;
        private readonly string _viberWebHook;
        public ViberProvider(IConfiguration configuration, ICacheHelper cacheHelper)
        {
            _viberToken = configuration["Viber"];
            _viberUrl = configuration["ViberUrl"];
            _viberWebHook = configuration["ViberWebHook"];
        }

        public async Task SetWebHook()
        {
            SetWebHookToken();
        }

        public async Task Remove()
        {
            var client = new RestClient(_viberUrl);
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("X-Viber-Auth-Token", _viberToken);
            request.AddJsonBody(new { url = "" });
            IRestResponse response = client.Execute(request);
        }

        public async Task Update(object res)
        {
            var asd = JsonConvert.DeserializeObject<CallBack>(res.ToString());
        }

        private void SetWebHookToken()
        {
            var client = new RestClient(_viberUrl);
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("X-Viber-Auth-Token", _viberToken);

            var postData = new SetWebHookViber();
            postData.send_name = true;
            postData.send_photo = true;
            postData.url = _viberWebHook;
            request.AddJsonBody(postData);

            IRestResponse response = client.Execute(request);
            var res = JsonConvert.DeserializeObject<ResponseWebHookViber>(response.Content);
        }

       
    }

    public class SetWebHookViber
    {
        public string url { get; set; }
        public bool send_name { get; set; }
        public bool send_photo { get; set; }
    }
    public class ResponseWebHookViber
    {
        public int Status { get; set; }
        public string Status_message { get; set; }
        public List<string> Event_types { get; set; } = new List<string>();
    }

    public class StartedMessage
    {
        public string receiver { get; set; }

        public string type { get; set; }

        public Keyboard keyboard { get; set; }
    }

    public class Keyboard
    {
        public string Type { get; set; }
        public bool DefaultHeight { get; set; }

        public List<Button> Buttons { get; set; } = new List<Button>();
    }

    public class Button
    {
        public string ActionType { get; set; }
        public string ActionBody { get; set; }
        public string Text { get; set; }
        public string TextSize { get; set; }
    }


    public class CallBack
    {
        public string Event { get; set; }
        public long timestamp { get; set; }
        public string type { get; set; }
        public string context { get; set; }
        public User user { get; set; }
        public bool subscribed { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string country { get; set; }
        public string language { get; set; }
        public int api_version { get; set; }
    }

}
