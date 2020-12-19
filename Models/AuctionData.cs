using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Auction.Models
{
    public class AuctionData
    {
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }
        [DisplayName("End date")]
        public DateTime EndDate { get; set; }
        public int Id { get; set; }
        public new string ToString => $"Starts: {StartDate.ToShortDateString()} Ends: {EndDate.ToShortDateString()}";
    }
}
