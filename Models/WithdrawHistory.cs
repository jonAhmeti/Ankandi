using System;

namespace Auction.Models
{
    public class WithdrawHistory
    {
        public int Id { get; set; }
        public DateTime WithdrawDate { get; set; }
        public decimal WithdrawAmount { get; set; }
        public int EventId { get; set; }
        public int AuctionId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
