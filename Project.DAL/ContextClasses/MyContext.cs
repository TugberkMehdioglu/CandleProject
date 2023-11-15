using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.ContextClasses
{
    public class MyContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);//For IdentityUserRole's primary key issue

            builder.ApplyConfigurationsFromAssembly(Assembly.Load("Project.MAP"));
        }

        public DbSet<Address>? Addresses { get; set; }
        public DbSet<AppUser>? AppUsers { get; set; }
        public DbSet<AppUserProfile>? AppUserProfiles { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderDetail>? OrderDetails { get; set; }
        public DbSet<Product>? Products { get; set; }

    }
}
