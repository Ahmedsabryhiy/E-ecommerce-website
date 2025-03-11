using BL;
using UI.ModelViews;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.ModelViews;

namespace UI.Controllers
{
    public class UsersController : Controller
    {
        UserManager<ApplicationUsers> oClsUserManager;
        SignInManager<ApplicationUsers> oClsSignInManager;
        public UsersController(UserManager<ApplicationUsers> oUserManager,
          SignInManager<ApplicationUsers> oSignInManager)
        {
            oClsUserManager = oUserManager;
            oClsSignInManager = oSignInManager;
        }
        public IActionResult Login(string returnUrl)
         {
            LoginModel model = new LoginModel()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        public IActionResult Logout()
        {
            oClsSignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View("Login", model);
            ApplicationUsers user = new ApplicationUsers()
            {

                Email = model.Email,
                UserName = model.Email

            };


            var sinInLogin = await oClsSignInManager.PasswordSignInAsync(user.Email, model.Password, true, true);

            try
            {
                if (sinInLogin.Succeeded)
                {

                    if (string.IsNullOrEmpty(model.ReturnUrl))
                        return RedirectToAction("Index", "Home");
                    else

                        return RedirectToAction(model.ReturnUrl);

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }




            return View(new LoginModel());
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View("Register", model);
            ApplicationUsers user = new ApplicationUsers()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email

            };
            var result = await oClsUserManager.CreateAsync(user, model.Password);
            try
            {
                if (result.Succeeded)
                {
                    var sinInLogin = await oClsSignInManager
                                   .PasswordSignInAsync(user.Email, model.Password, true, true);
                    try
                    {
                        if (sinInLogin.Succeeded)
                        {
                            return RedirectToAction("OrdersSuccess", "Orders");

                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }

            return View(new RegisterModel());
        }


    }
}
