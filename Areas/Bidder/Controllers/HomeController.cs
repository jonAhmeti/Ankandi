using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Areas.Bidder.Controllers
{
    [Area("Bidder")]
    [Route("Bidder")]
    public class HomeController : Controller
    {
        private readonly BLL.ActiveAuctions _bllActiveAuctions;
        private readonly BLL.Events _bllEvents;
        private readonly BLL.Items _bllItems;

        public HomeController(BLL.ActiveAuctions bllActiveAuctions, BLL.Events bllEvents, BLL.Items bllItems)
        {
            _bllActiveAuctions = bllActiveAuctions;
            _bllEvents = bllEvents;
            _bllItems = bllItems;
        }

        public async Task<IActionResult> Index()
        {
            var activeEvents = new List<Event>();
            var activeAuctions = Mapper.ActiveAuctionsMap(await _bllActiveAuctions.GetAllAsync());
            foreach (var obj in activeAuctions)
            {
                if (obj.Opened)
                {
                    activeEvents = Mapper.EventsMap(await _bllEvents.GetAllByAuctionId(obj.AuctionId)).ToList();
                }
            }

            var itemsBo = new List<BO.Item>();
            foreach (var obj in activeEvents)
            {
               itemsBo.Add(await _bllItems.GetAsync(obj.ItemId));
            }

            ViewBag.Items = Mapper.ItemsMap(itemsBo);
            return View(activeEvents);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(int id)
        {
            var objEvent = Mapper.Event(await _bllEvents.GetAsync(id));
            ViewBag.Event = objEvent;
            var objItem = Mapper.Item(await _bllItems.GetAsync(objEvent.ItemId));
            return View(objItem);
        }

        [HttpGet("GetActiveAuctionDetails")]
        public async Task<Dictionary<string, object>> GetActiveAuctionDetails()
        {
            var events = Mapper.EventsMap(await _bllEvents.GetAllByAuctionId(Mapper.ActiveAuctionsMap(await _bllActiveAuctions.GetAllAsync()).FirstOrDefault().AuctionId)).ToList();
            var items = new List<Item>();
            foreach (var objEvent in events)
            {
                items.Add(Mapper.Item(await _bllItems.GetAsync(objEvent.ItemId)));
            }

            return new Dictionary<string, object>()
            {
                {"events", events},
                {"items",items}
            };
        }

        [HttpGet("GetEventDetails")]
        public async Task<Event> GetEventDetails(int id)
        {
            return Mapper.Event(await _bllEvents.GetAsync(id));
        }
    }
}