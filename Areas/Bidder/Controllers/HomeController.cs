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
        private readonly BLL.Bids _bllBidHistories;
        private readonly BLL.Users _bllUsers;
        private readonly BLL.Withdrawals _bllWithdraws;

        public HomeController(BLL.ActiveAuctions bllActiveAuctions, BLL.Events bllEvents, BLL.Items bllItems, BLL.Bids bllBidHistories, BLL.Users bllUsers, BLL.Withdrawals bllWithdraws)
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
            var activeEvents = new List<Events>();
            var activeAuctions = Mapper.ActiveAuctionsMap(await _bllActiveAuctions.GetAllAsync());
            if (activeAuctions == null)
                return View(null);              // added now

            foreach (var obj in activeAuctions)
            {
                if (obj.Open)
                {
                    activeEvents = Mapper.EventsMap(await _bllEvents.GetAllByAuctionId(obj.AuctionId)).ToList();
                }
            }

            var itemsBo = new List<BO.Items>();
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
            var activeAuction = (await _bllActiveAuctions.GetAllAsync()).FirstOrDefault();
            var activeEvents = (await _bllEvents.GetAllByAuctionId(activeAuction.AuctionId));
            var items = new List<Items>();
            foreach (var objEvent in activeEvents)
            {
                items.Add(Mapper.Item(await _bllItems.GetAsync(objEvent.ItemId)));
            }

            if (activeAuction == null || activeAuction.Open == false || items.Count <= 0) // RE-check
            {
                return new Dictionary<string, object>()
                {
                    {"closed", null},
                    {"events", activeEvents},
                    {"items",items}
                };
            }

            return new Dictionary<string, object>()
            {
                {"events", activeEvents},
                {"items",items}
            };
        }

        //Method that returns the event object when 'Details' of an event/item is clicked in the bidder's home.
        [HttpGet("GetEventDetails")]
        public async Task<Events> GetEventDetails(int id)
        {
            var objEvent = Mapper.Event(await _bllEvents.GetAsync(id));
            int activeAuctionId = (await _bllActiveAuctions.GetAllAsync()).FirstOrDefault().AuctionId;

            string username = HttpContext.User.Identity.Name;

            if (string.IsNullOrWhiteSpace(username))
            {
                return objEvent;
            }

            var bidderId = (await _bllUsers.GetByUsernameAsync(username)).Id;

            //get top bidder for selected event in active auction that is NOT our bidder
            objEvent.TopBidder = (await _bllBidHistories.GetLatestUserBid(activeAuctionId, id)).FirstOrDefault().UserId;


            if (objEvent.TopBidder == null || objEvent.TopBidder == bidderId) return objEvent;

            objEvent.PriceChanged = true;
            return objEvent;
        }

        //Bid method that calls the bid procedure and returns the updated event.
        [HttpPost("Bid")]
        public async Task<Events> Bid(Bids obj)
        {
            var bidderId = (await _bllUsers.GetByUsernameAsync(HttpContext.User.Identity.Name)).Id;
            var objEvent = await GetEventDetails(obj.EventId);
            
            //if bidAmount has changed before refreshing on the user's screen and is smaller than other biders' bid then return null
            if (obj.BidAmount <= objEvent.CurrentPrice)
            {
                objEvent.PriceChanged = true;
                return objEvent;
            }

            //update event's currentPrice property and set TopBidder
            var successEvent = await _bllEvents.UpdateAsync(new BO.Events()
            {
                CurrentPrice = obj.BidAmount,
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
            var successBid = await _bllBidHistories.AddAsync(new BO.Bids()
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
        public async Task<Events> Withdraw(Withdrawals obj)
        {
            var bidderId = (await _bllUsers.GetByUsernameAsync(HttpContext.User.Identity.Name)).Id;
            var objEvent = await _bllEvents.GetAsync(obj.EventId);
            var latestBid = (await _bllBidHistories.GetTopBidderActiveEvent(obj.AuctionId, obj.EventId, bidderId)).FirstOrDefault();

            //if latest bidder is our bidder (or if there are no bids yet)
            if (latestBid == null || bidderId == latestBid.UserId) return null;
            var throwbackBidHistory = await _bllWithdraws.GetThrowbackAsync(new BO.Withdrawals()
            {
                UserId = bidderId,
                AuctionId = obj.AuctionId,
                EventId = obj.EventId
            });
            if (throwbackBidHistory == null || 
                throwbackBidHistory.UserId == 0 ||
                throwbackBidHistory.BidAmount > objEvent.CurrentPrice) 
                return null;
            //update throwback details ( topBidder and currentPrice for throwback bidder ) of event.
            var updatedEvent = new BO.Events()
            {
                CurrentPrice = throwbackBidHistory.BidAmount,
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
            var successWithdraw = await _bllWithdraws.AddAsync(new BO.Withdrawals()
            {
                AuctionId = obj.AuctionId,
                EventId = obj.EventId,
                UserId = bidderId,
                WithdrawAmount = latestBid.BidAmount
            });

            //Reverse Withdraw by setting highest OTHER user's bid as latest by biddate.
            await _bllBidHistories.AddAsync(throwbackBidHistory);
            var successEvent = await _bllEvents.UpdateAsync(updatedEvent);
            return Mapper.Event(updatedEvent);
        }
    }
}