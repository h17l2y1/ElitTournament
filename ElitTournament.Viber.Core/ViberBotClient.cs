using ElitTournament.Viber.Core.Enums;
using ElitTournament.Viber.Core.Models;
using ElitTournament.Viber.Core.Models.Interfaces;
using ElitTournament.Viber.Core.Models.Message;
using ElitTournament.Viber.Core.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ElitTournament.Viber.Core
{
	public class ViberBotClient : IViberBotClient, IDisposable
	{
		public const string XViberContentSignatureHeader = "X-Viber-Content-Signature";

		private const string XViberAuthTokenHeader = "X-Viber-Auth-Token";
		private const string BaseAddress = "https://chatapi.viber.com/pa/";
		private readonly HttpClient _httpClient;
		private readonly HMACSHA256 _hashAlgorithm;

		public ViberBotClient(string authenticationToken) : this(authenticationToken, null)
		{
		}

		public ViberBotClient(string authenticationToken, IWebProxy proxy)
		{
			_httpClient = proxy == null ? new HttpClient() : new HttpClient(new HttpClientHandler { Proxy = proxy, UseProxy = true });
			_httpClient.BaseAddress = new Uri(BaseAddress);
			_httpClient.DefaultRequestHeaders.Add(XViberAuthTokenHeader, new[] { authenticationToken });
			_hashAlgorithm = new HMACSHA256(Encoding.UTF8.GetBytes(authenticationToken));
		}

		public async Task<SetWebhookResponse> SetWebhookAsync(string url, ICollection<EventType> eventTypes = null, bool sendName = true, bool sendPhoto = true)
		{
			var webHookViberModel = new WebHookViber()
			{
				URL = url,
				EventTypes = eventTypes?.Select(x => x.ToString()).ToArray(),
				SendName = sendName,
				SendPhoto = sendPhoto,
			};

			string method = UrlTypes.set_webhook.ToString();

			SetWebhookResponse result = await RequestApiAsync<SetWebhookResponse>(method, webHookViberModel);
			return result;
		}

		public async Task RemoveWebhookAsync()
		{
			string method = UrlTypes.set_webhook.ToString();
			var result = await RequestApiAsync<SetWebhookResponse>(method, new { url = string.Empty });
		}

		public async Task<IAccountInfo> GetAccountInfoAsync()
		{
			string method = UrlTypes.get_account_info.ToString();
			return await RequestApiAsync<GetAccountInfoResponse>(method);
		}

		public Task<long> SendTextMessageAsync(TextMessage message) => SendMessageAsync(message);

		public Task<long> SendKeyboardMessageAsync(KeyboardMessage message) => SendMessageAsync(message);


		private async Task<long> SendMessageAsync(MessageBase message, bool isBroadcast = false)
		{
			var result = await RequestApiAsync<SendMessageResponse>(isBroadcast ? "broadcast_message" : "send_message", message);
			return result.MessageToken;
		}

		private async Task<T> RequestApiAsync<T>(string method, object data = null) where T : ApiResponseBase, new()
		{
			string requestJson = data == null ? "{}" : JsonConvert.SerializeObject(data);
			HttpResponseMessage response = await _httpClient.PostAsync(method, new StringContent(requestJson));
			string responseJson = await response.Content.ReadAsStringAsync();
			T result = JsonConvert.DeserializeObject<T>(responseJson);
			if (result.Status != ErrorCode.Ok)
			{
				throw new ViberRequestApiException(result.Status, result.StatusMessage, method, requestJson, responseJson);
			}

			return result;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_httpClient.Dispose();
				_hashAlgorithm.Dispose();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}
