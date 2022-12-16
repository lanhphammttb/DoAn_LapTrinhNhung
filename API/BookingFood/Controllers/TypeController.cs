using BookingFood.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;
using BookingFood.DTOs.TypeDto;
using Type = BookingFood.Entities.Type;

namespace BookingFood.Controllers
{
    [ApiController]
    [Route("api/type")]
    public class TypeController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TypeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("getall")]
        public async Task<ActionResult> GetAll()
        {
            var list = await (from t in _dataContext.Type
                              select new
                              {
                                  Name = t.Name,
                              }).AsNoTracking().ToListAsync();
            return Ok(list);
        }
        [HttpPost("create")]
        public async Task<ActionResult<TypeDto>> Create(CreateTypeDto input)
        {
            var newType = await _dataContext.Type.AsNoTracking().FirstOrDefaultAsync(e => e.Name == input.Name);
            if (newType != null) return BadRequest("Type is taken");
            var type = new Type
            {
                Id = new Guid(),
                Name = input.Name,
            };
            await _dataContext.Type.AddAsync(type);
            await _dataContext.SaveChangesAsync();
            return new TypeDto
            {
                Id = type.Id,
                Name = type.Name,
            };
        }
        [HttpPut("update")]
        public async Task<ActionResult> Update(TypeDto input)
        {
            var type = await _dataContext.Type.FindAsync(input.Id);
            if (type == null)
            {
                return BadRequest("Type not existed");
            }
            else
            {
                type.Name = input.Name;
                _dataContext.Type.Update(type);
                await _dataContext.SaveChangesAsync();
                return Ok(type);
            }
        }
        [HttpDelete("delete")]
        public async Task<ActionResult> Detele(Guid id)
        {
            _dataContext.Food.RemoveRange(await _dataContext.Food.Where(e => e.TypeId == id).ToListAsync());
            await _dataContext.SaveChangesAsync();
            _dataContext.Type.Remove(await _dataContext.Type.FindAsync(id));
            await _dataContext.SaveChangesAsync();
            return Ok("Removed");
        }
    }
}
