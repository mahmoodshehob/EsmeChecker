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
        Task<IEnumerable<Esme>> GetAllEsmes(string? filter = null);
        Task<IEnumerable<SmeInfo>> GetAllEsmesInfo(string? filter = null);
		Esme GetEsme(string systemID,string? filter = "");
		Task<string> GetEsmeAuth(string SN, int MoMt);
		Task<(string, List<Esme>)> NotifcationEsme();
		Task<Esme> QueryEsme(string systemID);
	}
}
