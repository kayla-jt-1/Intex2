using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Intex2.Models.ViewModels;
using Intex2.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

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
            //ViewBag.Crashes = repo.Crashes.ToList(); //IS THIS NECESSARY????
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
                //ViewBag.Crashes = repo.Crashes.ToList();
                return View();
            }
        }


        //EDIT CRASH 
        [HttpGet]
        public IActionResult Edit(int crashId)
        {
            var crash = repo.Crashes.Single(x => x.CRASH_ID == crashId);

            return View("AddCrash", crash);
        }

        [HttpPost]
        public IActionResult Edit(Crash crash)
        {
            if (ModelState.IsValid)
            {
                repo.SaveCrash(crash);

                // CHECK THIS 
                return RedirectToAction("CrashSummary");
                //return View("AdminHome");
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
            Crash crashChange = repo.Crashes.Single(x => x.CRASH_ID == crash.CRASH_ID);

            repo.DeleteCrash(crashChange);

            return RedirectToAction("AdminHome");
        }


        //LOOKUP CRASH FOR ADMINS
        [HttpGet]
        public IActionResult AdminLookup()
        {
            return View();
        }


        //FILTER BY CITY
        [HttpGet]
        public IActionResult CityFilter(string city)
        {
            var temp = repo.Crashes
                            .Where(x => x.CITY == city)
                            .ToList();

            return View("DisplayResults", temp);
        }

        //FILTER BY ID
        [HttpGet]
        public IActionResult IdFilter(int crashid)
        {
            var temp = repo.Crashes
                            .Where(x => x.CRASH_ID == crashid)
                            .ToList();
            return View("DisplayResults", temp);
        }

        //FILTER BY COUNTY
        [HttpGet]
        public IActionResult CountyFilter(string county)
        {
            var temp = repo.Crashes
                            .Where(x => x.COUNTY_NAME == county)
                            .ToList();
            return View("DisplayResults", temp);
        }

        //FILTER BY CRASH SEVERITY
        [HttpGet]
        public IActionResult CrashSevFilter(int severity)
        {
            var temp = repo.Crashes
                            .Where(x => x.CRASH_SEVERITY_ID == severity)
                            .ToList();
            return View("DisplayResults", temp);
        }

        //FILTER BY DUI
        [HttpGet]
        public IActionResult DUIFilter(string dui)
        {
            var temp = repo.Crashes
                            .Where(x => x.DUI == dui)
                            .ToList();
            return View("DisplayResults", temp);
        }

        //FILTER BY DISTRACTED DRIVING
        [HttpGet]
        public IActionResult DistractedFilter(string distracted)
        {
            var temp = repo.Crashes
                            .Where(x => x.DISTRACTED_DRIVING == distracted)
                            .ToList();
            return View("DisplayResults", temp);
        }
        



        //DISPLAYS FILTERED RESULTS
        [HttpGet]
        public IActionResult DisplayResults()
        {
            var blah = repo.Crashes
                            .ToList();

            return View(blah);
        }


        //ACTIONS FOR USER REGISTRATION
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }


        [Authorize]
        public IActionResult AdminHome()
        {
            return View(); 
        }



    }
}
