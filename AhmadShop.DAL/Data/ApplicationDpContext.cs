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
    public class ApplicationDpContext : DbContext
    {
        public DbSet<Category>Categories { get; set; }
        public DbSet<Brand>Brands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(
                "Data Source=.;Database=AhmadShop;Integrated Security=True;TrustServerCertificate=True;"
            );
        }
    }
}
