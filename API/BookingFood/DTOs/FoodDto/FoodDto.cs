using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace BookingFood.DTOs.FoodDto
{
    public class FoodDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Guid TypeId { get; set; }
    }
}
