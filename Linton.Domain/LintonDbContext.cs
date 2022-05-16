using Linton.Domain.Interfaces;
using Linton.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linton.Domain
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
