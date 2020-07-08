using AuthIdentity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthIdentity.Controllers
{
    public class HomeController : Controller

    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {

            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {

            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                //sign in

                var signInRsult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInRsult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }

        public IActionResult Register()
            {
                return View();
            }
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {

            //register User

            var user = new IdentityUser
                {
                    UserName = username,
                    Email = "",
                };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                //sign in
                     var signInRsult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                     if (signInRsult.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }

            }

            return RedirectToAction("Login");
        }
        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }

    }
}

