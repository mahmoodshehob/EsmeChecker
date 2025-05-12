using EsmeChecker.Models.Sybase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.BusinessRules.Interfaces
{
    public interface IEsmeDbServices
    {
		Task<List<Esme>> QueryAllEsme();
		Task<List<Esme>> NotifcationEsme();
        Task<List<Esme>> QueryNotifcationEsme();
		Task<Esme> QueryOneEsme(string systemId);


		Task GenerateMessageForOne(string msisdn, Esme esme);
		Task GenerateMessageForMulti(List<string> msisdn, List<Esme> esme_s);
		
	}
}
