using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.BusinessRules.Interfaces
{
    public interface IMainServices
    {
		Task<ContentResult> QueryEsmeServiceByUssd(string xmlContent);
        Task<ContentResult> QueryEsmeServiceDirect(string ussdServiceCode, string mSISDN, string ussdRequestString);
    }
}
