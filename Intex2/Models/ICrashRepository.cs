using System;
using System.Linq;

namespace Intex2.Models
{
    public interface ICrashRepository
    {
        IQueryable<Crash> Crashes { get; }

        public void SaveCrash(Crash c);
        public void CreateCrash(Crash c);
        public void DeleteCrash(Crash c);
    }
}
