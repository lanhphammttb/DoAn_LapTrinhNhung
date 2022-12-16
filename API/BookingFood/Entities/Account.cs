using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingFood.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<Bill> Bill { get; set; }
    }
}
