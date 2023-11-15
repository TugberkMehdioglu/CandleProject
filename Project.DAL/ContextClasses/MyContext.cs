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

            //DataSeed for appusers's identity
            SeedRoles(builder);
            SeedUserRoles(builder);
        }

        public DbSet<Address>? Addresses { get; set; }
        public DbSet<AppUser>? AppUsers { get; set; }
        public DbSet<AppUserProfile>? AppUserProfiles { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderDetail>? OrderDetails { get; set; }
        public DbSet<Product>? Products { get; set; }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
            new IdentityRole()
            {
                Id = "4d7b3bc1-f3aa-48ce-b587-5e7dc5557634",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityRole()
            {
                Id = "4d7b3bc1-f3aa-48ce-b587-5e7dc5553134",
                Name = "Member",
                NormalizedName = "MEMBER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>()
            {
                UserId = "5c8defd5-91f2-4256-9f16-e7fa7546dec4",
                RoleId = "4d7b3bc1-f3aa-48ce-b587-5e7dc5557634"
            },
            new IdentityUserRole<string>()
            {
                UserId = "5c8defd5-91f2-4256-9f16-e7fa7546fec5",
                RoleId = "4d7b3bc1-f3aa-48ce-b587-5e7dc5553134"
            });
        }
    }
}
