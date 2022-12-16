using System;

namespace BookingFood.DTOs.BillDto
{
    public class CreateBillDto
    {
        public Guid UserId { get; set; }
        public Guid FoodId { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
