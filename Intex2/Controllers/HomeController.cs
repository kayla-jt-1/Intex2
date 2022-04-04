using Intex2.Models;
using Microsoft.AspNetCore.Identity;
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

        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public HomeController(ICrashRepository temp, UserManager<IdentityUser> um, SignInManager<IdentityUser> sim)
        {
            repo = temp;
            userManager = um;
            signInManager = sim;
        }

        public IActionResult Index()
        {
            return View();
        }


        //VIEW ALL CRASHES
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

        //EDIT CRASH 
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


        //DELETE CRASH
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


        //LOGIN
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new Login { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(login.Username);

                if (user != null)
                {
                    await signInManager.SignOutAsync();

                    if ((await signInManager.PasswordSignInAsync(user, login.Password, false, false)).Succeeded)
                    {
                        return Redirect(login?.ReturnUrl ?? "/Admin");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid Name or Password");
            return View(login);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();

            return Redirect(returnUrl);
        }
    }
}
