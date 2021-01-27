using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    public class HomeController : Controller
    {
        private readonly BLL.BidHistories _bllBidHistories;
        private readonly BLL.Withdraws _bllWithdrawHistories;

        public HomeController(BLL.BidHistories bllBidHistories, BLL.Withdraws bllWithdrawHistories)
        {
            _bllBidHistories = bllBidHistories;
            _bllWithdrawHistories = bllWithdrawHistories;
        }
        public async Task<IActionResult> Index()
        {
            var historyBidWithdraw = new Dictionary<string, object>();
            historyBidWithdraw["bids"] = Mapper.BidsMap(await _bllBidHistories.GetActiveAuctionBidHistory());
            historyBidWithdraw["withdraws"] = Mapper.WithdrawsMap(await _bllWithdrawHistories.GetActiveAuctionWithdrawHistory());

            return View(historyBidWithdraw);
        }
    }
}