﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Intex2.Models;


namespace Intex2.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<IdentityUser> userManager; // we are setting the value of this in the AccountController constructor right below
        private SignInManager<IdentityUser> signInManager;

        public AdminController(UserManager<IdentityUser> um, SignInManager<IdentityUser> sim)
        {
            userManager = um;
            signInManager = sim;
        }

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

            ModelState.AddModelError("", "Invalid username or password");
            return View(login); 
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();

            return Redirect(returnUrl); 
        }

    }
}