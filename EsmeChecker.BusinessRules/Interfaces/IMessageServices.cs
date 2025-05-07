using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.BusinessRules.Interfaces
{
    public interface IMessageServices
    {
		Task SendSMS(string TargetNumber, string Voucher);
	}
}
