using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Intex2.Models.ViewModels;
using Intex2.Models;

namespace Intex2.Controllers
{
    public class AdminController : Controller
    {
        private ICrashRepository repo;

        private UserManager<IdentityUser> userManager; // we are setting the value of this in the AccountController constructor right below
        private SignInManager<IdentityUser> signInManager;

        public AdminController(ICrashRepository temp, UserManager<IdentityUser> um, SignInManager<IdentityUser> sim)
        {
            repo = temp;
            userManager = um;
            signInManager = sim;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel { ReturnUrl = returnUrl }); 
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginmodel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginmodel.Username);

                if (user != null)
                {
                    await signInManager.SignOutAsync();

                    if ((await signInManager.PasswordSignInAsync(user, loginmodel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginmodel?.ReturnUrl ?? "/Admin");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid Username or Password");
            return View(loginmodel); 
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();

            return Redirect(returnUrl); 
        }


        //ADD CRASH
        [HttpGet]
        public IActionResult AddCrash()
        {
            //ViewBag.Crashes = repo.Crashes.ToList();
            return View(new Crash());

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
                ViewBag.Crashes = repo.Crashes.ToList(); //DO WE NEED THIS LINE???
                return View();
            }
        }


        ////CONFIRMATION OF ADDING
        //[HttpGet]
        //public IActionResult Confirmation(Crash crash)
        //{

        //    return View();

        //}




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

                return RedirectToPage("CrashSummary");
            }
            else
            {
                return View("AddCrash", crash);
            }
        }

        

        [HttpGet]
        public IActionResult GroupFilter(int crashSevId)
        {
            var blah = repo.Crashes.Where(x => x.CRASH_SEVERITY_ID == crashSevId).ToList();
            return View("CrashSummary", blah);
        }


        //LOOKUP CRASH FOR ADMINS
        [HttpGet]
        public IActionResult AdminLookup()
        {
            return View();
        }



        [HttpPost]
        public IActionResult AdminLookup(Crash crash)
        { 

            return View(crash);
        }


        //public IActionResult Test()
        //{

        //    return View();
        //}

    }
}
