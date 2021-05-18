using BSBAdminDashboard.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.DAL.Database
{
    public class DbContainer : IdentityDbContext
    {
        public DbContainer(DbContextOptions<DbContainer> opts) : base(opts) { }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<District> District { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Server Name => DESKTOP-4A6TU94\\SQLEXPRESS
        //    optionsBuilder.UseSqlServer("server=DESKTOP-4A6TU94\\SQLEXPRESS;database=SharpDb;trusted_connection=true;");
        //}


    }
}
