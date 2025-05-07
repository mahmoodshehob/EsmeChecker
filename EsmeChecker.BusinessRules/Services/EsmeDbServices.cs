using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.Models.Sybase;
using EsmeChecker.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsmeChecker.DataAccess.Repository.IRepository;

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

			//Esme bbb = DbBiulder.CheckEsme("11555");

			try
			{
				//RedisService.TestRedis();
			}
			catch (Exception ex)
			{
				string issue = ex.Message;

			}

			List<Esme> result = await unitOfWork.SybaseRepository.QueryAllEsme();

			return result;
		}


		public async Task<List<Esme>> MaxMin()
		{
			(string Message, List<Esme> esme) = await unitOfWork.SybaseRepository.NotifcationEsme();

			return esme;
		}
	}
}
