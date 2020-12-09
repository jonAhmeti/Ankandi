using System;
using System.Collections.Generic;

namespace Auction.Models
{
    public partial class Users
    {
        public Users()
        {
            BidHistory = new HashSet<BidHistory>();
            Event = new HashSet<Event>();
            WithdrawHistory = new HashSet<WithdrawHistory>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime Lud { get; set; }
        public int Lun { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime InD { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<BidHistory> BidHistory { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<WithdrawHistory> WithdrawHistory { get; set; }
    }
}
