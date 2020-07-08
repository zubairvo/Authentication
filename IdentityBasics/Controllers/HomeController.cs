using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityBasics.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {

            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Zubi"),
                new Claim(ClaimTypes.Email, "Zubair.com"),
                
            };

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Zubi VO"),
                new Claim("Email Claim", "ZubairVO.com"),

            };
            var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

            var authIdentity = new ClaimsIdentity(authClaims, "User Auth");

            var userPrincipal = new ClaimsPrincipal(new[] { userIdentity, authIdentity });

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }

    }
}
