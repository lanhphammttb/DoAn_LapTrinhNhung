using System;

namespace BookingFood.DTOs.FoodDto
{
    public class CreateFoodDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Guid TypeId { get; set; }
    }
}
