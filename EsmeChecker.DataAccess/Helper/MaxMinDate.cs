using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsmeChecker.DataAccess.Repository;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Models.Helper;
using Microsoft.Extensions.Configuration;

namespace EsmeChecker.DataAccess.Helper
{
	public class MaxMinDate
	{

		public int MaxDateTimeSecond { get; set; }

		public int MinDateTimeSecond { get; set; }

		private readonly IUnitOfWork unitOfWork;


		private MaxMinModelDto _maxMinModel;

		private IConfiguration config;

		public MaxMinDate(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		
		}

			public async Task<MaxMinDate> GetMaxMinTime()
		{
			_maxMinModel = new MaxMinModelDto();
			try
			{
				//config.GetSection("MaxMinModel").Bind(_maxMinModel);
				var getMaxMin = await unitOfWork.MaxMinConfig.GetMaxMinConfig();
				if (getMaxMin == null) 
				{
					_maxMinModel.DayMin = getMaxMin.DayMin;
					_maxMinModel.DayMax = getMaxMin.DayMax;
					_maxMinModel.FixedHourMin = getMaxMin.FixedHourMin;
					_maxMinModel.FixedHourMax = getMaxMin.FixedHourMax;
				}
			}
			catch
			{
				_maxMinModel = new MaxMinModelDto();
			}



			DateTime now = DateTime.Now;
			DateTime StartDateTime = new DateTime(1994, 1, 1, 0, 0, 0);



			// the date of today at time 00:00:00
			DateTime MaxDateTime = new DateTime(now.Year, now.Month, now.Day, _maxMinModel.FixedHourMax, 0, 0);

			TimeSpan MaxtimeSpan = MaxDateTime - StartDateTime;
			// the date of today at time 00:00:00 by second
			double MaxDateTimeSecond = MaxtimeSpan.TotalSeconds + (_maxMinModel.DayMax * 86400);




			// the date of DayBack at time 00:00:00
			DateTime MinDateTime = new DateTime(now.Year, now.Month, now.Day, _maxMinModel.FixedHourMin, 0, 0);
			TimeSpan MintimeSpan = MinDateTime - StartDateTime;
			// the date of DayBack at time 00:00:00 by second
			double MinDateTimeSecond = MintimeSpan.TotalSeconds - (_maxMinModel.DayMin * 86400);


			var dd = MaxDateTimeSecond - MinDateTimeSecond;


			int maxDateTimeSecond_int = Convert.ToInt32(MaxDateTimeSecond);

			int minDateTimeSecond_int = Convert.ToInt32(MinDateTimeSecond);

			return
				new MaxMinDate(unitOfWork)
				{
					MaxDateTimeSecond = maxDateTimeSecond_int,
					MinDateTimeSecond = minDateTimeSecond_int
				};
		}

	}
}
