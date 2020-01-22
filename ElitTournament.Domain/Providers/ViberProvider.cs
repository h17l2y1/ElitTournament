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
}
