using Intex2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Controllers
{
    public class HomeController : Controller
    {

        private ICrashRepository repo;

        public HomeController(ICrashRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }


        // VIEW ALL CRASHES
        [HttpGet]
        public IActionResult AllCrashes()
        {
            return View();
        }


        //ADD CRASH
        [HttpGet]
        public IActionResult AddCrash()
        {
            ViewBag.Crashes = repo.Crashes.ToList();
            return View();

        }

        [HttpPost]
        public IActionResult AddCrash(Crash crash)
        {
            if (ModelState.IsValid)
            {
                repo.SaveCrash(crash);

                return View("Confirmation", crash);
            }
            else
            {
                ViewBag.Crashes = repo.Crashes.ToList();
                return View();
            }
        }

        // EDIT CRASH 
        [HttpGet]
        public IActionResult EditCrash(int crashId)
        {
            var crash = repo.Crashes.Single(x => x.CRASH_ID == crashId);

            return View("AddCrash", crash);
        }

        [HttpPost]
        public IActionResult EditCrash(Crash crash)
        {
            if (ModelState.IsValid)
            {
                repo.SaveCrash(crash);

                return RedirectToAction("CrashSummary");
            }
            else
            {
                return View("AddCrash", crash);
            }
        }


        // DELETE CRASH
        [HttpGet]
        public IActionResult Delete(int crashId)
        {
            var crash = repo.Crashes.Single(x => x.CRASH_ID == crashId);

            return View(crash);
        }

        [HttpPost]
        public IActionResult Delete(Crash crash)
        {
            repo.DeleteCrash(crash);

            return View("CrashSummary");
        }
    }
}
