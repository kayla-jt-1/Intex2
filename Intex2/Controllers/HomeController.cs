using Intex2.Models;
using Intex2.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
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

        // I THINK THIS JUST NEEDS TO BE IN ADMINCONTROLLER.CS
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;


        private InferenceSession _session;

        public HomeController(ICrashRepository temp, UserManager<IdentityUser> um, SignInManager<IdentityUser> sim, InferenceSession session)
        {
            repo = temp;
            userManager = um;
            signInManager = sim;
            _session = session;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Predict(string prediction) //string prediction
        {
            ViewBag.PredictionResults = prediction;

            return View();
        }

        [HttpPost]
        public IActionResult Predict(CrashData data)
        {
            
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });

            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First()};
            result.Dispose();

            ViewBag.PredictedResults = Math.Round(prediction.PredictedValue);

            return View("PredictResults", prediction.PredictedValue);
        }

        public IActionResult PredictResults(int prediction)
        {
            ViewBag.PredictedResults = prediction;

            return View();
        }


        //VIEW ALL CRASHES
        [HttpGet]
        public IActionResult AllCrashes(int pageNum = 1)
        {
            int resultsPerPage = 10;

            var x = new AccidentsViewModel
            {
                Crashes = repo.Crashes
                .OrderBy(x => x.CRASH_DATETIME)
                .Skip((pageNum - 1) * resultsPerPage)
                .Take(resultsPerPage),

                PageInfo = new PageInfo
                {
                    TotalNumRecords = repo.Crashes.Count(),
                    RecordsPerPage = resultsPerPage,
                    CurrentPage = pageNum,
                    LinksPerPage = 10
                }
            };

                return View("CrashSummary", x);
        }


        ////ADD CRASH
        //[HttpGet]
        //public IActionResult AddCrash()
        //{
        //    ViewBag.Crashes = repo.Crashes.ToList();
        //    return View();

        //}

        //[HttpPost]
        //public IActionResult AddCrash(Crash crash)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repo.SaveCrash(crash);

        //        return View("Confirmation", crash);
        //    }
        //    else
        //    {
        //        ViewBag.Crashes = repo.Crashes.ToList();
        //        return View();
        //    }
        //}


        ////EDIT CRASH 
        //[HttpGet]
        //public IActionResult Edit(int crashId)
        //{
        //    var crash = repo.Crashes.Single(x => x.CRASH_ID == crashId);

        //    return View("AddCrash", crash);
        //}

        //[HttpPost]
        //public IActionResult Edit(Crash crash)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repo.SaveCrash(crash);

        //        return RedirectToAction("CrashSummary");
        //    }
        //    else
        //    {
        //        return View("AddCrash", crash);
        //    }
        //}


        ////DELETE CRASH
        //[HttpGet]
        //public IActionResult Delete(int crashId)
        //{
        //    var crash = repo.Crashes.Single(x => x.CRASH_ID == crashId);

        //    return View(crash);
        //}

        //[HttpPost]
        //public IActionResult Delete(Crash crash)
        //{
        //    repo.DeleteCrash(crash);

        //    return View("CrashSummary");
        //}


        ////LOGIN
        //[HttpGet]
        //public IActionResult Login(string returnUrl)
        //{
        //    return View(new Login { ReturnUrl = returnUrl });
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(Login login)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IdentityUser user = await userManager.FindByNameAsync(login.Username);

        //        if (user != null)
        //        {
        //            await signInManager.SignOutAsync();

        //            if ((await signInManager.PasswordSignInAsync(user, login.Password, false, false)).Succeeded)
        //            {
        //                return Redirect(login?.ReturnUrl ?? "/Admin");
        //            }
        //        }
        //    }

        //    ModelState.AddModelError("", "Invalid Name or Password");
        //    return View(login);
        //}

        //public async Task<RedirectResult> Logout(string returnUrl = "/")
        //{
        //    await signInManager.SignOutAsync();

        //    return Redirect(returnUrl);
        //}


        //PRIVACY
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
