using System;
using System.Collections.Generic;

namespace Auction.Models
{
    public partial class Event
    {
        public Event()
        {
            BidHistory = new HashSet<BidHistory>();
            WithdrawHistory = new HashSet<WithdrawHistory>();
        }

        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Lun { get; set; }
        public DateTime? Lud { get; set; }
        public int? TopBidder { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal MinPriceIncrementAmount { get; set; }
        public int? ItemId { get; set; }
        public int? AuctionId { get; set; }

        public virtual AuctionData Auction { get; set; }
        public virtual Item Item { get; set; }
        public virtual Users TopBidderNavigation { get; set; }
        public virtual ICollection<BidHistory> BidHistory { get; set; }
        public virtual ICollection<WithdrawHistory> WithdrawHistory { get; set; }
    }
}
