using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.DataAccess.Repository;
using EsmeChecker.Entities;
using EsmeChecker.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsmeChecker.BusinessRules.Interfaces;

namespace EsmeChecker.BusinessRules.Services
{
    public class MaxMinConfigService : IMaxMinConfigService
    {
        private readonly IUnitOfWork unitOfWork;

        public MaxMinConfigService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<MaxMinModelDto> GetConfig()
        {
            MaxMinConfig result = await unitOfWork.MaxMinConfig.GetMaxMinConfig();

            return new()
            {
                DayMax = result.DayMax,
                FixedHourMax = result.FixedHourMax,
                DayMin = result.DayMin,
                FixedHourMin = result.FixedHourMin

            };
        }
    }
}