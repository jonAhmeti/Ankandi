using System;
using System.Collections.Generic;

namespace Auction.Models
{
    public partial class Items
    {
        public Items()
        {
            Events = new HashSet<Events>();
        }

        public int Id { get; set; }
        public decimal StartPrice { get; set; }
        public string Details { get; set; }
        public decimal? SoldPrice { get; set; }
        public string Name { get; set; }
        public DateTime? SoldDate { get; set; }
        public string MeasurementUnit { get; set; }
        public int Amount { get; set; }
        public DateTime Lud { get; set; }
        public int Lun { get; set; }
        public string Image { get; set; }
        public DateTime InD { get; set; }

        public virtual ICollection<Events> Events { get; set; }
    }
}
