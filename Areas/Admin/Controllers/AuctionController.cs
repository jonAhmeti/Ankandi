using System;
using System.Threading.Tasks;
using Auction.Models;
using Microsoft.AspNetCore.Mvc;
using Auctions = Auction.Models.Auctions;

namespace Auction.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Auction")]
    public class AuctionController : Controller
    {
        private readonly BLL.Auctions _bllAuctionData;
        private readonly BLL.ActiveAuctions _bllActiveAuction;

        public AuctionController(BLL.Auctions bllAuctionData, BLL.ActiveAuctions bllActiveAuction)
        {
            _bllAuctionData = bllAuctionData;
            _bllActiveAuction = bllActiveAuction;
        }

        public async Task<IActionResult> Index()
        {
            var auctions = await _bllAuctionData.GetAllAsync();
            ViewBag.Auctions = auctions;
            ViewBag.ActiveAuctionInfo = await _bllActiveAuction.GetAllAsync();
            ViewBag.ActiveAuction = await _bllAuctionData.GetAsync(ViewBag.ActiveAuctionInfo[0].AuctionId);
            return View(Mapper.AuctionDataMap(auctions));
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(Auctions obj)
        {
            var result = await _bllAuctionData.AddAsync(new BO.Auctions()
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
            return PartialView("AuctionDataPartial/_AuctionDataEditForm", new Auctions()
            {
                StartDate = DateTime.Parse(obj.StartDate.ToString()),
                EndDate = DateTime.Parse(obj.EndDate.ToString())
            });
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Auctions obj)
        {
            var result = await _bllAuctionData.UpdateAsync(new BO.Auctions()
            {
                Id = obj.Id,
                StartDate = obj.StartDate,
                EndDate = obj.EndDate
            });
            return RedirectToAction("Index");
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bllAuctionData.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost("SetActive")]
        public async Task<IActionResult> SetActive(int id)
        {
            if (id <= 0) return RedirectToAction("Index");

            //Delete Current Active Auction From Database
            var successDelete = await _bllActiveAuction.DeleteAsync();

            //Add Selected Active Auction To DB
            var successAdd = await _bllActiveAuction.AddAsync(new BO.ActiveAuctions()
                {AuctionId = id, Open = false, ClosedBy = 1, OpenedBy = 1});
            return RedirectToAction("Index");
        }

        [HttpPost("Open")]
        public async Task<bool> Open()
        {
            return await _bllActiveAuction.Open();
        }

        [HttpPost("Close")]
        public async Task<bool> Close()
        {
            return await _bllActiveAuction.Close();
        }
    }
}