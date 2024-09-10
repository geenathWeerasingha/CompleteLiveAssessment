using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using LiveAssessment.Models;

namespace LiveAssessment.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Variation> Variations { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                         
            modelBuilder.Entity<Product>().HasKey(u => u.Id);
            modelBuilder.Entity<Variation>().HasKey(u => u.VariationId);
             

        }
    }
}
