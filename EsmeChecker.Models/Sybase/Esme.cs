using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.Models.Sybase
{
	public class Esme
	{
		public string System_Id { get; set; }
		public string? Description { get; set; }
		public string? Password { get; set; }
		public string Activeenabletime { get; set; }
		public string Activeexpiry { get; set; }
	}
}
