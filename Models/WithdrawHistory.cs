using System;
using System.Collections.Generic;

namespace Auction.Models
{
    public partial class WithdrawHistory
    {
        public int Id { get; set; }
        public DateTime? WithdrawDate { get; set; }
        public decimal WithdrawAmount { get; set; }
        public int? EventId { get; set; }
        public int? AuctionId { get; set; }
        public int UserId { get; set; }

        public virtual AuctionData Auction { get; set; }
        public virtual Event Event { get; set; }
        public virtual Users User { get; set; }
    }
}
