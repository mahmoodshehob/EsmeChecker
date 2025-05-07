﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.Models.Kannel
{
	public class SmsToKannel
	{
		public string Sender { get; set; }
		public string Receiver { get; set; }
		public string Message { get; set; }
		public string? Profile { get; set; }
	}
}
