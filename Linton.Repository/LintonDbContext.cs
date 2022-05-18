using Linton.Domain.Models;
using Microsoft.EntityFrameworkCore; 
namespace Linton.Repository
{
    public class LintonDbContext : DbContext
    {
        public LintonDbContext(DbContextOptions<LintonDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(localdb)\Local;Initial Catalog=LintonGroup;Integrated Security=True");
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Bank> Banks { get; set; }
         
    }
}
