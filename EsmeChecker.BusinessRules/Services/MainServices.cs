using Azure;
using EsmeChecker.BusinessRules.Helper;
using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.DataAccess.Data;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Models.Sybase;
using EsmeChecker.Models.Ussd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.BusinessRules.Services
{
    public class MainServices : IMainServices
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IUnitOfServices unitOfServices;
		

		public MainServices(IConfiguration config, IUnitOfWork unitOfWork,IUnitOfServices unitOfServices) 
		{ 
			this.unitOfWork = unitOfWork;
			this.unitOfServices = unitOfServices;
		}

		private const string contentType = "text/xml";

		public async Task<ContentResult> QueryEsmeService(string xmlContent) 
        {

			ContentResult response = new ContentResult();

			var multiRequest = await UssdConverter.ParseRequest(xmlContent);



			Esme esme = await unitOfWork.SybaseRepository.QueryEsme(multiRequest.USSDRequestString);



			MultiResponseUSSD multiResponse = new MultiResponseUSSD()
			{
				TransactionId = multiRequest.TransactionId,
			};

			if (esme.Activeenabletime == "1994-01-01 12:00:00 AM")
				esme.Activeenabletime = "unKnown";



			if (esme.System_Id != null)
			{

				multiResponse.USSDResponseString = "You Will Receive Details in Message";
				string Message =
					"Short Number : " + esme.System_Id + "\n\n" +
					"Service Name : \n" + esme.Description + "\n\n" +
					"Active Time : \n" + esme.Activeenabletime + "\n\n" +
					"Expiry Time : \n" + esme.Activeexpiry;

				response = new ContentResult
				{
					ContentType = contentType,
					Content = UssdConverter.CreateResponses.Success(multiResponse),
					StatusCode = 200
				};

				// Submit SMS

				await unitOfServices.MessageServices.SendSMS(multiRequest.MSISDN, Message);
			}
			else if (esme.System_Id == null && esme.Description == "ErrorCode#0004 : Pool timed out trying to reserve a connection")
			{
				multiResponse.USSDResponseString = "Database Connection timed out";

				response = new ContentResult
				{
					ContentType = contentType,
					Content = UssdConverter.CreateResponses.Success(multiResponse),
					StatusCode = 200
				};
			}
			else
			{

				multiResponse.USSDResponseString = "The Entered Short Number not Exist";
			}

			response = new ContentResult
			{
				ContentType = contentType,
				Content = UssdConverter.CreateResponses.Success(multiResponse),
				StatusCode = 200
			};

			testDB(multiRequest.USSDRequestString);
			return response;
        }

		private void testDB(string systemID)
		{
			var r = unitOfWork.SybaseRepository.CheckEsme(systemID);
		}



		//private static void QueryInSMS(string Target, string Message)
		//{
		//	List<string> Targets = new List<string>() { };
		//	Targets.Add(Target);

		//	try
		//	{
		//		Process.SmsActions smsActions = new Process.SmsActions();
		//		if (Message != null)
		//		{
		//			smsActions.PostSms(Targets, Message);
		//		}

		//	}
		//	catch { }
		//}

	}
}
