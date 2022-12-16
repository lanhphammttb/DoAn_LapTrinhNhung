using BookingFood.Data;
using BookingFood.DTOs.FoodDto;
using BookingFood.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;
using BookingFood.DTOs.BillDto;

namespace BookingFood.Controllers
{
    [ApiController]
    [Route("api/bill")]
    public class BillController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public BillController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("getall")]
        public async Task<ActionResult> GetAll(string name)
        {
            var list = await (from b in _dataContext.Bill
                              join a in _dataContext.Account on b.UserId equals a.Id
                              join f in _dataContext.Food on b.FoodId equals f.Id
                              select new
                              {
                                  User = a.Name,
                                  Food = f.Name,
                                  Quantity = b.Quantity,
                                  TotalPrice = b.TotalPrice
                              }).AsNoTracking().ToListAsync();
            return Ok(list);
        }
        [HttpPost("create")]
        public async Task<ActionResult<BillDto>> Create(CreateBillDto input)
        {
            var bill = new Bill
            {
                Id = new Guid(),
                UserId = input.UserId,
                FoodId = input.FoodId,
                Quantity = input.Quantity,
                TotalPrice = input.TotalPrice,
            };
            await _dataContext.Bill.AddAsync(bill);
            await _dataContext.SaveChangesAsync();
            return new BillDto
            {
                Id = bill.Id,
                UserId = bill.UserId,
                FoodId= bill.FoodId,
                Quantity= input.Quantity,
                TotalPrice= input.TotalPrice,
            };
        }
        [HttpPut("update")]
        public async Task<ActionResult> Update(BillDto input)
        {
            var bill = await _dataContext.Bill.FindAsync(input.Id);
            if (bill == null)
            {
                return BadRequest("Bill not existed");
            }
            else
            {
                bill.UserId = input.UserId;
                bill.FoodId = input.FoodId;
                bill.Quantity = input.Quantity;
                bill.TotalPrice = input.TotalPrice;
                _dataContext.Bill.Update(bill);
                await _dataContext.SaveChangesAsync();
                return Ok(bill);
            }
        }
        [HttpDelete("delete")]
        public async Task<ActionResult> Detele(Guid id)
        {
            _dataContext.Bill.Remove(await _dataContext.Bill.FindAsync(id));
            await _dataContext.SaveChangesAsync();
            return Ok("Removed");
        }
    }
}
