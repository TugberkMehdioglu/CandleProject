﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configurations
{
    public class OrderDetailConfiguration : BaseConfiguration<OrderDetail>
    {
        public override void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            base.Configure(builder);

            builder.Ignore(x => x.Id);
            builder.HasKey(x => new
            {
                x.OrderID,
                x.ProductID
            });

            builder.Property(x => x.SubTotal).HasPrecision(18,2);
        }
    }
}
