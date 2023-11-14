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
        }
    }
}
