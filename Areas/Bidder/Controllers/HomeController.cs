using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Areas.Bidder.Controllers
{
    [Area("Bidder")]
    [Route("Bidder")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}