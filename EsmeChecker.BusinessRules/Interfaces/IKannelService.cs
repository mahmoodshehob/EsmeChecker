using EsmeChecker.Models.Kannel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.BusinessRules.Interfaces
{
    public interface IKannelService
    {
		Task<KannelStatusDto> Status();
		Task SendSMS(string targetNumber, string messageContent);
	}
}
