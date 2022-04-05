using Intex2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Component
{
    public class TypesViewComponent : ViewComponent
    {
        private ICrashRepository repo {get;set;}
        public TypesViewComponent (ICrashRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            var types = repo.Crashes
                .Select(x => x)
                .Distinct()
                .OrderBy(x => x);

                return View(types);
        }
    }
}
