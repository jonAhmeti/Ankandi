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
        private readonly BLL.BidHistories _bllBidHistories;
        private readonly BLL.Users _bllUsers;
        private readonly BLL.Withdraws _bllWithdraws;

        public HomeController(BLL.ActiveAuctions bllActiveAuctions, BLL.Events bllEvents, BLL.Items bllItems, BLL.BidHistories bllBidHistories, BLL.Users bllUsers, BLL.Withdraws bllWithdraws)
        {
            _bllActiveAuctions = bllActiveAuctions;
            _bllEvents = bllEvents;
            _bllItems = bllItems;
            _bllBidHistories = bllBidHistories;
            _bllUsers = bllUsers;
            _bllWithdraws = bllWithdraws;
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

        //Method that returns the currently Active Auction's events and items for the corresponding events.
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

        //Method that returns the event object when 'Details' of an event/item is clicked in the bidder's home.
        [HttpGet("GetEventDetails")]
        public async Task<Event> GetEventDetails(int id)
        {
            return Mapper.Event(await _bllEvents.GetAsync(id));
        }

        //Bid method that calls the bid procedure and returns the updated event.
        [HttpPost("Bid")]
        public async Task<Event> Bid(BidHistory obj)
        {
            var bidderId = (await _bllUsers.GetByUsernameAsync(obj.Username)).Id;
            var objEvent = await GetEventDetails(obj.EventId);
            
            //if bidAmount has changed before refreshing on the user's screen and is smaller than other biders' bid then return null
            if (obj.BidAmount < objEvent.CurrentPrice)
            {
                return objEvent;
            }

            //update event's currentPrice property and set TopBidder
            var successEvent = await _bllEvents.UpdateAsync(new BO.Event()
            {
                CurrentPrice = obj.BidAmount,
                TopBidder = bidderId,
                Id = objEvent.Id,
                ItemId = objEvent.ItemId,
                AuctionId = objEvent.AuctionId,
                StartDate = objEvent.StartDate,
                EndDate = objEvent.EndDate,
                Lun = objEvent.Lun,
                Lud = objEvent.Lud,
                MinPriceIncrementAmount = objEvent.MinPriceIncrementAmount
            });

            //register bidHistory
            var successBid = await _bllBidHistories.AddAsync(new BO.BidHistory()
            {
                AuctionId = obj.AuctionId,
                EventId = obj.EventId,
                UserId = bidderId,
                BidAmount = obj.BidAmount,
                BidDate = DateTime.Now
            });

            var x = Mapper.Event(await _bllEvents.GetAsync(obj.EventId));
            return x;
        }

        //Withdraw method that registers user's bid withdrawal and sets back currentPrice to highest other bidder.
        [HttpPost("Withdraw")]
        public async Task<Event> Withdraw(WithdrawHistory obj)
        {
            var bidderId = (await _bllUsers.GetByUsernameAsync(obj.Username)).Id;
            var objEvent = await _bllEvents.GetAsync(obj.EventId);
            var latestBid = await _bllBidHistories.GetLatestUserBid(bidderId, obj.AuctionId, obj.EventId);

            //if latest bidder isn't our bidder (or if there are no bids yet), no need to use our processor any further and cause some unknown catastrophic error.
            if (latestBid == null || bidderId != latestBid.UserId) return null;
            var throwbackBidHistory = await _bllWithdraws.GetThrowbackAsync(new BO.WithdrawHistory()
            {
                UserId = bidderId,
                AuctionId = obj.AuctionId,
                EventId = obj.EventId
            });
            if (throwbackBidHistory.UserId == 0 || throwbackBidHistory.BidAmount > objEvent.CurrentPrice) return null;
            //update throwback details ( topBidder and currentPrice for throwback bidder ) of event.
            var updatedEvent = new BO.Event()
            {
                CurrentPrice = throwbackBidHistory.BidAmount,
                TopBidder = throwbackBidHistory.UserId,
                Id = objEvent.Id,
                ItemId = objEvent.ItemId,
                AuctionId = objEvent.AuctionId,
                StartDate = objEvent.StartDate,
                EndDate = objEvent.EndDate,
                Lun = objEvent.Lun,
                Lud = objEvent.Lud,
                MinPriceIncrementAmount = objEvent.MinPriceIncrementAmount
            };

            //Register withdraw history
            var successWithdraw = await _bllWithdraws.AddAsync(new BO.WithdrawHistory()
            {
                AuctionId = obj.AuctionId,
                EventId = obj.EventId,
                UserId = bidderId,
                WithdrawAmount = latestBid.BidAmount,
                WithdrawDate = DateTime.Now
            });
            var successEvent = await _bllEvents.UpdateAsync(updatedEvent);
            return Mapper.Event(updatedEvent);
        }
    }
}