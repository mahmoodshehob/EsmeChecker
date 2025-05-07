using EsmeChecker.Models.Sybase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.DataAccess.Repository.IRepository
{
    public interface ISybaseRepository
    {
		Task<List<Esme>> QueryAllEsme();
		Task<Esme> CheckEsme(string systemID);
		Task<string> EsmeAuth(string SN, int MoMt);
		Task<(string, List<Esme>)> NotifcationEsme();
		Task<Esme> QueryEsme(string systemID);
	}
}
