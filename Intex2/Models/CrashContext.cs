using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public class CrashContext : DbContext
    {
        //Constructor 
        public CrashContext(DbContextOptions<CrashContext> options) : base(options)
        {

        }
        public DbSet<Crash> Crashes { get; set; }

    }
}










//The stuff below is supposed to help connect to our MySQL database

//using System.Data.Entity;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;

//namespace Intex2.Models
//{
//    public class CrashDBContext : DbContext
//    {
//        public CrashDBContext()
//          : base(GetRDSConnectionString())
//        {
//        }

//        public static CrashDBContext Create()
//        {
//            return new CrashDBContext();
//        }
//    }
//}