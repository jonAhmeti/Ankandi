using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Auction.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BLL.Items _bllItems;

        public HomeController(ILogger<HomeController> logger, BLL.Items bllItems)
        {
            _logger = logger;
            _bllItems = bllItems;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
