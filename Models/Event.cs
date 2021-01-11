using System;
using System.ComponentModel;

namespace Auction.Models
{
    public class Event
    {
        public int Id { get; set; }
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }
        [DisplayName("End date")]
        public DateTime EndDate { get; set; }
        public int Lun { get; set; }
        public DateTime? Lud { get; set; }
        [DisplayName("Top bidder")]
        public int? TopBidder { get; set; }
        [DisplayName("Current price")]
        public decimal CurrentPrice { get; set; }
        [DisplayName("Min price increment by")]
        public decimal MinPriceIncrementAmount { get; set; }
        [DisplayName("Item's id")]
        public int ItemId { get; set; }
        [DisplayName("Auction's id")]
        public int AuctionId { get; set; }

        public bool PriceChanged { get; set; }
    }
}
