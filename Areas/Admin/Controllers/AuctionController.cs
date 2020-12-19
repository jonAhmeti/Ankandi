using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Models;
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
            return View(Mapper.AuctionDataMap(await _bllAuctionData.GetAllAsync()));
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(AuctionData obj)
        {
            return RedirectToAction("Index");
        }
    }
}