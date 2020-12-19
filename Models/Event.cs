using System;

namespace Auction.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Lun { get; set; }
        public DateTime? Lud { get; set; }
        public int? TopBidder { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal MinPriceIncrementAmount { get; set; }
        public int ItemId { get; set; }
        public int AuctionId { get; set; }
    }
}
