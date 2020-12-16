using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Auction")]
    public class AuctionController : Controller
    {
        private readonly BLL.AuctionData _bllAuctionData;

        public AuctionController(BLL.AuctionData bllAuctionData)
        {
            _bllAuctionData = bllAuctionData;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _bllAuctionData.GetAllAsync());
        }
    }
}