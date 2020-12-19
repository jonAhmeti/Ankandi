using System;

namespace Auction.Models
{
    public class Users
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime? Lud { get; set; }
        public int? Lun { get; set; }
        public DateTime Dob { get; set; }
        public DateTime InD { get; set; }
    }
}
