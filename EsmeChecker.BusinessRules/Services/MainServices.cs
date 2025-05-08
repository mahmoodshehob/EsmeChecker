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


        private async Task<MultiResponseUSSD> QueryEsmeService(MultiRequestUSSD multiRequest)
        {
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

                // Submit SMS
                await unitOfServices.MessageServices.SendSMS(multiRequest.MSISDN, Message);

                return multiResponse;
            }
            else if (esme.System_Id == null && esme.Description == "ErrorCode#0004 : Pool timed out trying to reserve a connection")
            {
                multiResponse.USSDResponseString = "Database Connection timed out";

                return multiResponse;
            }
            else
            {

                multiResponse.USSDResponseString = "The Entered Short Number not Exist";
            }

            testDB(multiRequest.USSDRequestString);
            return multiResponse;
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















        public async Task<ContentResult> QueryEsmeServiceByUssd(string xmlContent)
        {
            ContentResult response = new ContentResult();

            MultiResponseUSSD multiResponse = await QueryEsmeService(await UssdConverter.ParseRequest(xmlContent));

            return new ContentResult
            {
                ContentType = contentType,
                Content = UssdConverter.CreateResponses.Success(multiResponse),
                StatusCode = 200
            };
        }

        public async Task<ContentResult> QueryEsmeServiceDirect(string ussdServiceCode,string mSISDN, string ussdRequestString)
        {
            ContentResult response = new ContentResult();

            MultiRequestUSSD multiRequest = new()
            {
                TransactionId = new Random().Next().ToString(),
                TransactionTime = DateTime.Now.ToString("yyyyMMddTHH:mm:ss"),
                USSDServiceCode = ussdServiceCode,
                MSISDN = mSISDN,
                USSDRequestString= ussdRequestString,
                Response=""            
            };

            MultiResponseUSSD multiResponse = await QueryEsmeService(multiRequest);

            return new ContentResult
            {
                ContentType = contentType,
                Content = UssdConverter.CreateResponses.Success(multiResponse),
                StatusCode = 200
            };
        }
    }
}
