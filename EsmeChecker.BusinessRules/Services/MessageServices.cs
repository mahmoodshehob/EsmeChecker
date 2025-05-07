using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.Models.Kannel;
using Microsoft.Extensions.Configuration;

namespace EsmeChecker.BusinessRules.Services
{
	public class MessageServices : IMessageServices
	{
		private readonly KannelModel _kannelModel;
		private readonly HttpClient _client;


		public MessageServices(IConfiguration config, IUnitOfServices unitOfServices) 
		{
			_client = new HttpClient();

			_kannelModel = new KannelModel();
			config.GetSection("KannelPara").Bind(_kannelModel);
		}


		public async Task SendSMS(string targetNumber, string messageContent)
		{
			//await _loggerG.LogCampaignAsync(TargetNumber + "|" + Voucher);
			try
			{
				var url = $"http://{_kannelModel.HostName}:{_kannelModel.Port}/cgi-bin/sendsms?username=kannel&password=kannel&smsc=ivas_campaign&from={_kannelModel.Sender}&to=%2B{targetNumber}&charset=UTF-8&coding=2&text={messageContent}";

				var request = new HttpRequestMessage(HttpMethod.Get, url);
				var response = await _client.SendAsync(request);
				response.EnsureSuccessStatusCode();
				//await _loggerG.LogCampaignAsync(TargetNumber + "|" + response.EnsureSuccessStatusCode());

			}
			catch (Exception ex)
			{
				//await _loggerG.LogCampaignAsync(TargetNumber + "|" + ex.Message);
			}
		}
	}
}
