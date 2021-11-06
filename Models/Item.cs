using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Auction.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Start price")]
        public decimal StartPrice { get; set; }

        [Required]
        public string Details { get; set; }

        [DisplayName("Price sold")]
        public decimal? SoldPrice { get; set; }

        [DisplayName("Sold date")]
        public DateTime? SoldDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Measurement units")]
        public string MeasurementUnits { get; set; }

        [Required]
        public int Amount { get; set; }
        public DateTime? InD { get; set; }
        public DateTime? Lud { get; set; }
        public int? Lun { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
