using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configurations
{
    public class AppUserConfiguration : BaseConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.AppUserProfile).WithOne(x => x.AppUser).HasForeignKey<AppUserProfile>(x => x.Id);

            List<AppUser> users = new()
            {
                new()
                {
                    Id="5c8defd5-91f2-4256-9f16-e7fa7546dec4",
                    SecurityStamp=Guid.NewGuid().ToString(),
                    UserName="Admin",
                    NormalizedUserName="ADMIN",
                    Email="Admin@gmail.com",
                    NormalizedEmail="ADMIN@GMAIL.COM",
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    PhoneNumber="5312292928",
                    LockoutEnabled=true,
                    CreatedDate=DateTime.Now,
                    Status=DataStatus.Inserted,
                    EmailConfirmed=true,
                    PhoneNumberConfirmed=true
                },
                new()
                {
                    Id="5c8defd5-91f2-4256-9f16-e7fa7546fec5",
                    SecurityStamp=Guid.NewGuid().ToString(),
                    UserName="Member",
                    NormalizedUserName="MEMBER",
                    Email="Member@gmail.com",
                    NormalizedEmail="MEMBER@GMAIL.COM",
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    PhoneNumber="5446340539",
                    LockoutEnabled=true,
                    CreatedDate=DateTime.Now,
                    Status=DataStatus.Inserted,
                    EmailConfirmed=true,
                    PhoneNumberConfirmed=true
                }
            };

            PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();
            users[0].PasswordHash = hasher.HashPassword(users[0], "Password12*");
            users[1].PasswordHash = hasher.HashPassword(users[1], "Password12*");

            builder.HasData(users);
        }
    }
}
