using System;

namespace Auction.Models
{
    public class Item
    {
        public int Id { get; set; }
        public decimal StartPrice { get; set; }
        public string Details { get; set; }
        public decimal? SoldPrice { get; set; }
        public DateTime? SoldDate { get; set; }
        public string Name { get; set; }
        public string MeasurementUnits { get; set; }
        public double Amount { get; set; }
        public DateTime? InD { get; set; }
        public DateTime? Lud { get; set; }
        public int? Lun { get; set; }
        public string Image { get; set; }
    }
}
