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
    public class AppUserProfileConfiguration : BaseConfiguration<AppUserProfile>
    {
        public override void Configure(EntityTypeBuilder<AppUserProfile> builder)
        {
            base.Configure(builder);

            builder.Ignore(x => x.FullName);//Will not be in database

            builder.HasMany(x => x.Orders).WithOne(x => x.AppUserProfile).HasForeignKey(x => x.AppUserProfileID);//OneToMany Relationship

            HashSet<AppUserProfile> profiles = new()
            {
                new()
                {
                     Id = "5c8defd5-91f2-4256-9f16-e7fa7546dec4",
                     FirstName = "Tuğberk",
                     LastName = "Mehdioğlu",
                     CreatedDate = DateTime.Now,
                     Status = DataStatus.Inserted
                },
                new()
                {
                    Id = "5c8defd5-91f2-4256-9f16-e7fa7546fec5",
                    FirstName = "Dilan",
                    LastName = "Polat",
                    CreatedDate = DateTime.Now,
                    Status = DataStatus.Inserted
                }
            };

            builder.HasData(profiles);
        }
    }
}
