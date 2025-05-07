//using EsmeChecker.DataAccess.Data;
//using EsmeChecker.DataAccess.Repository.IRepository;
//using EsmeChecker.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace EsmeChecker.DataAccess.Repository
//{
//    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
//    {
//        private readonly ApplicationDbContext _dbContext;

//        public ApplicationUserRepository(ApplicationDbContext dbContext) : base(dbContext)
//        {
//            _dbContext = dbContext;

//        }

//        public void Save()
//        {
//            _dbContext.SaveChanges();

//        }

//        public void Update(ApplicationUser applicationUser)
//        {
//            _dbContext.ApplicationUsers.Update(applicationUser);
//        }
//    }
//}
