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

		public EsmeDbServices(IUnitOfWork unitOfWork , IUnitOfServices unitOfServices) 
		{
			this.unitOfWork = unitOfWork;
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


		public async Task<List<Esme>> NotifcationEsme()
		{


			(string Message, List<Esme> esme) = await unitOfWork.SybaseRepository.NotifcationEsme();

			return esme;
		}


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
            return esme;
        }
    }
}
