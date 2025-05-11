using EsmeChecker.DataAccess.Helper;
using EsmeChecker.Models.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.BusinessRules.Helper
{
    public class GetPeriodBySecond
    {

        public int MaxDateTimeSecond { get; set; }

        public int MinDateTimeSecond { get; set; }

        private MaxMinModelDto _maxMinModel;

        private IConfiguration config;

        public static MaxMinDate GetMaxMinTime(MaxMinModelDto? _maxMinModel)
        {
            if (_maxMinModel==null)
                _maxMinModel = new();

            DateTime now = DateTime.Now;
            DateTime StartDateTime = new DateTime(1994, 1, 1, 0, 0, 0);



            // the date of today at time 00:00:00
            DateTime MaxDateTime = new DateTime(now.Year, now.Month, now.Day, _maxMinModel.FixedHourMax, 0, 0).AddDays(+_maxMinModel.DayMax);

            //TimeSpan MaxtimeSpan = MaxDateTime - StartDateTime;
            //// the date of today at time 00:00:00 by second
            //double MaxDateTimeSecond = MaxtimeSpan.TotalSeconds;

            // the Max expiry date by second
            double MaxDateTimeSecond = (MaxDateTime - StartDateTime).TotalSeconds;


            //// the date of DayBack at time 00:00:00
            //DateTime MinDateTime = new DateTime(now.Year, now.Month, now.Day, _maxMinModel.FixedHourMin, 0, 0);
            //TimeSpan MintimeSpan = MinDateTime - StartDateTime;
            //// the date of DayBack at time 00:00:00 by second
            //double MinDateTimeSecond = MintimeSpan.TotalSeconds - (_maxMinModel.DayMin * 86400);

            DateTime MinDateTime = new DateTime(now.Year, now.Month, now.Day, _maxMinModel.FixedHourMin, 0, 0).AddDays(-_maxMinModel.DayMin);
            double MinDateTimeSecond = (MinDateTime - StartDateTime).TotalSeconds;



            var dd = MaxDateTimeSecond - MinDateTimeSecond;


            int maxDateTimeSecond_int = Convert.ToInt32(MaxDateTimeSecond);

            int minDateTimeSecond_int = Convert.ToInt32(MinDateTimeSecond);

            return
                new MaxMinDate()
                {
                    MaxDateTimeSecond = maxDateTimeSecond_int,
                    MinDateTimeSecond = minDateTimeSecond_int
                };
        }

    }
}
