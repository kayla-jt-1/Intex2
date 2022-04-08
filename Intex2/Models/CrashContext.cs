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
