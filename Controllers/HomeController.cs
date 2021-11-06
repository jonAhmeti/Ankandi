using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.BLL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Auction.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Users _bllUsers;

        public HomeController(ILogger<HomeController> logger, BLL.Users bllUsers)
        {
            _logger = logger;
            _bllUsers = bllUsers;
        }

        [Route("")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string area = User.Identities.FirstOrDefault()
                    ?.Claims
                    .FirstOrDefault(claim => claim.Type == ClaimTypes.Role)
                    ?.Value;

                return RedirectToAction("Index", "Home", new {area});
            }
            return View();
        }

        //________________________________________________________________________________________________________//
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(7)
                });

            return LocalRedirect(returnUrl);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _bllUsers.GetByUsernameAsync(username);
            //var user = new BO.Users() {
            //    Id = 1,
            //    Name = "TempName",
            //    RoleId = 2,
            //    Username = "TempUsername",
            //    Password = "a"
            //};
            if (user != null && user.Password == password)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.RoleId == 1 ? "Admin" : "Bidder")
                };
                var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);

                var area = user?.RoleId == 1 ? "Admin" : "Bidder";

                return RedirectToAction("Index", "Home", new { area });
            }

            return RedirectToAction("Index");
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(string username, string password, DateTime dob, string name)
        {
            await _bllUsers.AddAsync(new BO.Users()
            {
                Username = username,
                Password = password,
                DoB = dob,
                Name = name,
                RoleId = 2
            });
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
