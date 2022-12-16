using System;
using System.ComponentModel.DataAnnotations;

namespace BookingFood.DTOs.AccountDto
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
