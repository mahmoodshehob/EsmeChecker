using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.Models.Sybase;
using EsmeChecker.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Models.Helper;
using EsmeChecker.BusinessRules.Helper;

namespace EsmeChecker.BusinessRules.Services
{
    public class EsmeDbServices : IEsmeDbServices
	{

		private readonly IUnitOfWork unitOfWork;
		private readonly IUnitOfServices unitOfServices;

		public EsmeDbServices(IUnitOfWork unitOfWork , IUnitOfServices unitOfServices) 
		{
			this.unitOfWork = unitOfWork;
			this.unitOfServices = unitOfServices;
		}

		public async Task<List<Esme>> QueryAllEsme()
		{
			try
			{
				//RedisService.TestRedis();
			}
			catch (Exception ex)
			{
				string issue = ex.Message;

			}

			var result = await unitOfWork.SybaseRepository.GetAllEsmes();

			return result.ToList();
		}

		public async Task<Esme> QueryOneEsme(string systemId)
		{
			try
			{
				return await unitOfWork.SybaseRepository.QueryEsme(systemId);
			}
			catch (Exception ex)
			{
				string issue = ex.Message;
			}

			return null;
		}

		//public async Task<List<Esme>> NotifcationEsme()
		//{
		//	(string Message, List<Esme> esme) = await unitOfWork.SybaseRepository.NotifcationEsme();

		//	return esme;
		//}

        public async Task<List<Esme>> QueryNotifcationEsme()
        {
			MaxMinConfig periodConfigration = await unitOfWork.MaxMinConfig.GetMaxMinConfig();

			MaxMinModelDto periodConfigrationDto = new()
			{
				DayMax = periodConfigration.DayMax,
				DayMin= periodConfigration.DayMin,
				FixedHourMax= periodConfigration.FixedHourMax,
				FixedHourMin= periodConfigration.FixedHourMin
            };

			var period = GetPeriodBySecond.GetMaxMinTime(periodConfigrationDto);

            string filter = $"activeexpiry <= " + period.MaxDateTimeSecond + " and activeexpiry >= " + period.MinDateTimeSecond ;

            var result=  await unitOfWork.SybaseRepository.GetAllEsmes(filter);

           // var result2 = await unitOfWork.SybaseRepository.GetAllEsmesInfo();

            List<Esme> esme = result.ToList();

			//get the msisdns will recieve notifcation
			var emplowees = await unitOfWork.Emplowee.GetAll(e=>e.Allow==true);

			await GenerateMessage(emplowees.ToList(), esme);

			return esme;
        }


		public async Task GenerateMessage(List<Emplowee> emplowees, List<Esme> esme_s)
		{
			string Message =
				"Short Number : " + esme_s[0].System_Id + "\n\n" +
				"Service Name : \n" + esme_s[0].Description + "\n\n" +
				"Active Time : \n" + esme_s[0].Activeenabletime + "\n\n" +
				"Expiry Time : \n" + esme_s[0].Activeexpiry;


			if (esme_s.Count > 1)
			{
				for (int i = 1; i < esme_s.Count; i++)
				{
					Message = Message + "\n---------------\n\n";
					Message = Message +
						"Short Number : " + esme_s[i].System_Id + "\n\n" +
						"Service Name : \n" + esme_s[i].Description + "\n\n" +
						"Active Time : \n" + esme_s[i].Activeenabletime + "\n\n" +
						"Expiry Time : \n" + esme_s[i].Activeexpiry;
				}
			}

			for (int i = 0; i < emplowees.Count; i++)
			{
				await unitOfServices.KannelService.SendSMS(emplowees[i].Msisdn, Message);
			}
		}
	}
}
