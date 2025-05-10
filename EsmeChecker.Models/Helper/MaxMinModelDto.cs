using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.Models.Helper
{
    public class MaxMinModelDto
    {
		public int FixedHourMax { get; set; } = 23;
		public int FixedHourMin { get; set; } = 0;
		public int DayMax { get; set; } = 6;
		public int DayMin { get; set; } = 1;
	}
}
