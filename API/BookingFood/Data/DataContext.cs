using BookingFood.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingFood.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Type> Type { get; set; }
        public DbSet<Bill> Bill { get; set; }
    }
}
