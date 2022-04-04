using System;
using System.Linq;

namespace Intex2.Models
{
    public class EFCrashRepository : ICrashRepository
    {
        private CrashContext context { get; set; }

        public EFCrashRepository(CrashContext c)
        {
            context = c;
        }

        public IQueryable<Crash> Crashes => context.Crashes;

        public void CreateCrash(Crash c)
        {
            context.Add(c);
            context.SaveChanges();
        }

        public void DeleteCrash(Crash c)
        {
            context.Remove(c);
            context.SaveChanges();
        }

        public void SaveCrash(Crash c)
        {
            context.Update(c);
            context.SaveChanges();
        }
    }
}