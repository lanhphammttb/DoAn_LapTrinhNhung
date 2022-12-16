using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingFood.Entities
{
    public class Food
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [ForeignKey("Type")]
        public Guid TypeId { get; set; }
        public ICollection<Bill> Bill { get; set; }
    }
}
