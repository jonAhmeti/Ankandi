using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.BLL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _bllUsers.GetByUsernameAsync(username);
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
            }

            var area = user?.RoleId == 1 ? "Admin" : "Bidder";

            return RedirectToAction("Index", "Home", new { area });
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
