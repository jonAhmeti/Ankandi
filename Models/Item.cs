using System;
using System.Collections.Generic;

namespace Auction.Models
{
    public partial class Item
    {
        public Item()
        {
            Event = new HashSet<Event>();
        }

        public int Id { get; set; }
        public decimal StartPrice { get; set; }
        public string Details { get; set; }
        public decimal? SoldPrice { get; set; }
        public DateTime? SoldDate { get; set; }
        public string Name { get; set; }
        public string MeasurementUnits { get; set; }
        public double Amount { get; set; }
        public bool? Sold { get; set; }
        public DateTime? InD { get; set; }
        public DateTime? Lud { get; set; }
        public int? Lun { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
