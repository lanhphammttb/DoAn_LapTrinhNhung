using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingFood.Entities
{
    public class Bill
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Account")]
        public Guid UserId { get; set; }
        [ForeignKey("Food")]
        public Guid FoodId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int TotalPrice { get; set; }
    }
}
