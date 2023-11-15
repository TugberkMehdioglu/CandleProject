using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configurations
{
    public class AddressConfiguration : BaseConfiguration<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder.Ignore(x => x.FullAddress);//Will not be in database.

            builder.HasOne(x => x.AppUserProfile).WithMany(x => x.Addresses).HasForeignKey(x => x.AppUserProfileID);//OneToMany relationship
            builder.HasMany(x => x.Orders).WithOne(x => x.Address).HasForeignKey(x => x.AddressID);//OneToMany relationship

            HashSet<Address> addresses = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Ev",
                    Country = "Türkiye",
                    City = "İstanbul",
                    District = "Kağıthane",
                    Neighborhood = "Çeliktepe",
                    Street = "ŞaşatuŞalat",
                    AptNo = 11,
                    Flat = 8,
                    AppUserProfileID = "5c8defd5-91f2-4256-9f16-e7fa7546dec4",
                    CreatedDate = DateTime.Now,
                    Status = ENTITIES.Enums.DataStatus.Inserted
                },
                new()
                {
                    Id = 2,
                    Name = "İş Yeri",
                    Country = "Türkiye",
                    City = "İstanbul",
                    District = "Beşiktaş",
                    Neighborhood = "Nispetiye",
                    Street = "Aydın",
                    AptNo = 7,
                    AppUserProfileID = "5c8defd5-91f2-4256-9f16-e7fa7546dec4",
                    CreatedDate = DateTime.Now,
                    Status = ENTITIES.Enums.DataStatus.Inserted
                },
                new()
                {
                    Id = 3,
                    Name = "Ev",
                    Country = "Türkiye",
                    City = "İstanbul",
                    District = "Ataşehir",
                    Neighborhood = "Küçükbakkalköy",
                    Street = "Rüya",
                    AptNo = 9,
                    AppUserProfileID = "5c8defd5-91f2-4256-9f16-e7fa7546fec5",
                    CreatedDate = DateTime.Now,
                    Status = ENTITIES.Enums.DataStatus.Inserted
                }
            };

            builder.HasData(addresses);
        }
    }
}
