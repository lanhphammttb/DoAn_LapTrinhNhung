using BookingFood.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookingFood.DTOs.AccountDto;
using BookingFood.Entities;

namespace BookingFood.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public AccountController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("getall")]
        public async Task<ActionResult> GetAll()
        {
            var list = await (from a in _dataContext.Account
                                  select new
                                  {
                                      Name = a.Name,
                                      Email = a.Email,
                                      Password = a.Password
                                  }).AsNoTracking().ToListAsync();
            return Ok(list);
        }
        [HttpPost("register")]
        public async Task<ActionResult<AccountDto>> Register(RegisterDto input)
        {
            var newUser = await _dataContext.Account.AsNoTracking().FirstOrDefaultAsync(e => e.Email == input.Email);
            if (newUser != null) return BadRequest("Username is taken");
            var user = new Account
            {
                Id = new Guid(),
                Name = input.Name,
                Email = input.Email,
                Password = input.Password,
            };
            await _dataContext.Account.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return new AccountDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<AccountDto>> Login(LoginDto input)
        {
            var user = await _dataContext.Account.AsNoTracking().FirstOrDefaultAsync(e => e.Email == input.Email);
            if (user == null) return Unauthorized("Invalid username");
            var pass = await _dataContext.Account.AsNoTracking().FirstOrDefaultAsync(e => e.Email == input.Email && e.Password == input.Password);
            if (pass == null) return Unauthorized("Invalid password");
            return new AccountDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }
    }
}
