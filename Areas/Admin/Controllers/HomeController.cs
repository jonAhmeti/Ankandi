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
        private readonly BLL.Bids _bllBidHistories;
        private readonly BLL.Withdrawals _bllWithdrawHistories;
        private readonly BLL.ActiveAuctions _bllActiveAuctions;

        public HomeController(BLL.Bids bllBidHistories, BLL.Withdrawals bllWithdrawHistories, BLL.ActiveAuctions activeAuctions)
        {
            _bllBidHistories = bllBidHistories;
            _bllWithdrawHistories = bllWithdrawHistories;
            _bllActiveAuctions = activeAuctions;
        }
        public async Task<IActionResult> Index()
        {
            var activeAcutions = Mapper.ActiveAuctionsMap(await _bllActiveAuctions.GetAllAsync());


            var historyBidWithdraw = new Dictionary<string, object> { { "bids", new List<Bids>() }, { "withdrawals", new List<Withdrawals>() } };
            foreach (var activeAuction in activeAcutions)
            {
                var bids = Mapper.BidsMap(await _bllBidHistories.GetActiveAuctionBidHistory(activeAuction.AuctionId));
                var withdrawals = Mapper.WithdrawsMap(await _bllWithdrawHistories.GetActiveAuctionWithdrawHistory(activeAuction.AuctionId));
                if (bids != null)
                    ((List<Bids>)historyBidWithdraw["bids"]).AddRange(bids);
                if (withdrawals != null)
                    ((List<Withdrawals>)historyBidWithdraw["withdrawals"]).AddRange(withdrawals);
            }
            //historyBidWithdraw["withdraws"] = Mapper.WithdrawsMap(await _bllWithdrawHistories.GetActiveAuctionWithdrawHistory());

            return View(historyBidWithdraw);
        }
    }
}