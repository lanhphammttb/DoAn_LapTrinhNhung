using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingFood.Entities
{
    public class Type
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Account> Account { get; set; }
    }
}
