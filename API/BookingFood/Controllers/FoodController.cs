using BookingFood.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookingFood.Entities;
using BookingFood.DTOs.TypeDto;
using BookingFood.DTOs.FoodDto;

namespace BookingFood.Controllers
{
    [ApiController]
    [Route("api/food")]
    public class FoodController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public FoodController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("getall")]
        public async Task<ActionResult> GetAll()
        {
            var list = await (from f in _dataContext.Food
                                  join t in _dataContext.Type on f.TypeId equals t.Id
                                  select new
                                  {
                                      Name = f.Name,
                                      Price = f.Price,
                                      Type = t.Name
                                  }).AsNoTracking().ToListAsync();
            return Ok(list);
        }
        [HttpPost("create")]
        public async Task<ActionResult<FoodDto>> Create(CreateFoodDto input)
        {
            var newFood = await _dataContext.Food.AsNoTracking().FirstOrDefaultAsync(e => e.Name == input.Name);
            if (newFood != null) return BadRequest("Food is taken");
            var food = new Food
            {
                Id = new Guid(),
                Name = input.Name,
                Price = input.Price,
                TypeId = input.TypeId,
            };
            await _dataContext.Food.AddAsync(food);
            await _dataContext.SaveChangesAsync();
            return new FoodDto
            {
                Id = food.Id,
                Name = food.Name,
                Price = food.Price,
                TypeId = food.TypeId,
            };
        }
        [HttpPut("update")]
        public async Task<ActionResult> Update(FoodDto input)
        {
            var food = await _dataContext.Food.FindAsync(input.Id);
            if (food == null)
            {
                return BadRequest("Food not existed");
            }
            else
            {
                food.Name = input.Name;
                food.Price = input.Price;
                food.TypeId = input.TypeId;
                _dataContext.Food.Update(food);
                await _dataContext.SaveChangesAsync();
                return Ok(food);
            }
        }
        [HttpDelete("delete")]
        public async Task<ActionResult> Detele(Guid id)
        {
            _dataContext.Food.Remove(await _dataContext.Food.FindAsync(id));
            await _dataContext.SaveChangesAsync();
            return Ok("Removed");
        }
    }
}
