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

        public HomeController()
        {

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

            return View();
        }

        [HttpPost]
        public IActionResult AddCrash(Crash crash)
        {
            return View("Index");
        }


        // EDIT CRASH 
        [HttpGet]
        public IActionResult EditCrash()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditCrash(Crash crash)
        {
            return RedirectToAction();
        }


        // DELETE CRASH
        [HttpGet]
        public IActionResult Delete()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Delete(Crash crash)
        {

            return View();
        }
    }
}
