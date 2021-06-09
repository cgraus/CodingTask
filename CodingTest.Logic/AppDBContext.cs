using CodingTest.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodingTest.Logic
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options = null)
           : base(options ?? new DbContextOptions<AppDBContext>())
        {
        }

        public DbSet<ProductModel> ProductModels { get; set; }
    }
}
