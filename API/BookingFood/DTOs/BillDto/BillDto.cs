using System;

namespace BookingFood.DTOs.BillDto
{
    public class BillDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid FoodId { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
