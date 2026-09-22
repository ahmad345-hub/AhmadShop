using AhmadShop.DAL.Model;
using Microsoft.Azure.Cosmos.Core.Collections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace AhmadShop.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category>Categories { get; set; }
        public DbSet<Brand>Brands { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        
    }
}
