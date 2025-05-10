using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.Models.Helper
{
	public class PaginatedModel
	{
		public int PageSize { get; set; } = 10;
		public int PageNumber { get; set; } = 1;
		public string Value { get; set; } = "";
		public int Id { get; set; }
	}
}
