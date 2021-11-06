using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.BLL;
using Auction.Models;
using Microsoft.AspNetCore.Mvc;
using AuctionData = Auction.BLL.AuctionData;

namespace Auction.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Event")]
    public class EventController : Controller
    {
        private readonly Events _bllEvents;
        private readonly Items _bllItems;
        private readonly AuctionData _bllAuctionData;

        public EventController(BLL.Events bllEvents, BLL.Items bllItems, BLL.AuctionData bllAuctionData)
        {
            _bllEvents = bllEvents;
            _bllItems = bllItems;
            _bllAuctionData = bllAuctionData;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Auctions"] = Mapper.AuctionDataMap(await _bllAuctionData.GetAllAsync());
            ViewData["Items"] = Mapper.ItemsMap(await _bllItems.GetAllAsync());

            return View(Mapper.EventsMap(await _bllEvents.GetAllAsync()));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Event obj)
        {
            var item = await _bllItems.GetAsync(obj.ItemId);
            var result = await _bllEvents.AddAsync(new BO.Event()
            {
                StartDate = obj.StartDate,
                EndDate = obj.EndDate,
                ItemId = obj.ItemId,
                AuctionId = obj.AuctionId,
                CurrentPrice = obj.CurrentPrice < item.StartPrice ? item.StartPrice : obj.CurrentPrice,
                MinPriceIncrementAmount = obj.MinPriceIncrementAmount
            });
            return RedirectToAction("Index");
        }

        [HttpGet("Edit")]
        public async Task<PartialViewResult> Edit(int id)
        {
            var obj = await _bllEvents.GetAsync(id);
            return PartialView("EventPartial/_EventEditForm", new Event()
            {
                Id = obj.Id,
                AuctionId = obj.AuctionId,
                ItemId = obj.ItemId,
                StartDate = obj.StartDate,
                EndDate = obj.EndDate,
                MinPriceIncrementAmount = obj.MinPriceIncrementAmount,
                CurrentPrice = obj.CurrentPrice,
                Lud = obj.Lud,
                Lun = obj.Lun
            });
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Event obj)
        {
            var result = await _bllEvents.UpdateAsync(new BO.Event()
            {
                Id = obj.Id,
                AuctionId = obj.AuctionId,
                ItemId = obj.ItemId,
                StartDate = obj.StartDate,
                EndDate = obj.EndDate,
                MinPriceIncrementAmount = obj.MinPriceIncrementAmount,
                CurrentPrice = obj.CurrentPrice
            });
            return RedirectToAction("Index");
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bllEvents.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}