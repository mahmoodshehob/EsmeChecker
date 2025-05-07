using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsmeChecker.Models.Helper;
using Microsoft.Extensions.Configuration;

namespace EsmeChecker.DataAccess.Helper
{
	public class MaxMinDate
	{

		public int MaxDateTimeSecond { get; set; }

		public int MinDateTimeSecond { get; set; }

		private MaxMinModel _maxMinModel = new MaxMinModel();

		private IConfiguration config;



		public MaxMinDate GetMaxMinTime()
		{

			try
			{
				config.GetSection("MaxMinModel").Bind(_maxMinModel);
			}
			catch
			{
				_maxMinModel = new MaxMinModel();
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
				new MaxMinDate
				{
					MaxDateTimeSecond = maxDateTimeSecond_int,
					MinDateTimeSecond = minDateTimeSecond_int
				};
		}

	}
}
