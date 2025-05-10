using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.BusinessRules.Helper;
using EsmeChecker.Models.Kannel;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EsmeChecker.BusinessRules.Services
{
	public class KannelService : IKannelService
	{

		private readonly KannelModel _kannelModel;
		private readonly HttpClient _client;
		public KannelService(IConfiguration config, IUnitOfServices unitOfServices)
		{
			_client = new HttpClient();

			_kannelModel = new KannelModel();
			config.GetSection("KannelPara").Bind(_kannelModel);
		}	


		public async Task<KannelStatusDto> Status()
		{
			//await _loggerG.LogCampaignAsync(TargetNumber + "|" + Voucher);
			try
			{
				string url = $"http://{_kannelModel.HostName}:13000/status?password={_kannelModel.Password}";
				var request = new HttpRequestMessage(HttpMethod.Get,url);
				var response = await _client.SendAsync(request);
				response.EnsureSuccessStatusCode();
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					return KannelHelper.MappingKannelStatus(content);
				}

				return new KannelStatusDto();
			}
			catch (Exception ex)
			{
				//await _loggerG.LogCampaignAsync(TargetNumber + "|" + ex.Message);

				return new KannelStatusDto();
			}
		}


		public async Task SendSMS(string targetNumber, string messageContent)
		{
			//await _loggerG.LogCampaignAsync(TargetNumber + "|" + Voucher);
			try
			{
				_kannelModel.Sender = _kannelModel.Sender.Replace("_", "%5F");

				var url = $"http://{_kannelModel.HostName}:{_kannelModel.Port}/cgi-bin/sendsms?username=kannel&password=kannel&smsc=ivas_cmservice&from={_kannelModel.Sender}&to=%2B{targetNumber}&charset=UTF-8&coding=2&text={messageContent}";

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
