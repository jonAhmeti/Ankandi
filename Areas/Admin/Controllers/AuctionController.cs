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
            await _bllAuctionData.AddAsync(new BO.AuctionData()
            {
                StartDate = obj.StartDate,
                EndDate = obj.EndDate
            });
            return RedirectToAction("Index");
        }

        [HttpGet("Edit")]
        public async Task<PartialViewResult> Edit(int id)
        {
            var obj = await _bllAuctionData.GetAsync(id);
            return PartialView("AuctionData/_AuctionDataEditForm", new AuctionData()
            {
                StartDate = DateTime.Parse(obj.StartDate.ToString()),
                EndDate = DateTime.Parse(obj.EndDate.ToString())
            });
        }
    }
}