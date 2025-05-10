using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.DataAccess.Data;
using EsmeChecker.DataAccess.Repository;
using EsmeChecker.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.BusinessRules.Services
{
    public class UnitOfServices : IUnitOfServices
	{
		public IEsmeDbServices EsmeDbServices { get; private set; }
		public IMainServices MainServices { get; private set; }
		public IEmploweeServcie EmploweeServcie { get; private set; }
		public IKannelService KannelService { get; private set; }

		public UnitOfServices(IConfiguration config , IUnitOfWork unitOfWork)
		{
			MainServices = new MainServices(config, unitOfWork, this);
			EsmeDbServices = new EsmeDbServices(unitOfWork,this);
			EmploweeServcie = new EmploweeServcie(unitOfWork);


			KannelService = new KannelService(config,this);
		}

	}
}
