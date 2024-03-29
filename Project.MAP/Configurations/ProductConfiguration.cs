﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configurations
{
    public class ProductConfiguration : BaseConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Price).HasPrecision(18,2);

            builder.HasMany(x => x.Photos).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);

            List<Product> products = new()
            {
                new(){ Id=1, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, Name="Dr. Vranjes Firenze", Description="Oud Nobile Sprey Cam Şişe Koku 100 ml", Price=1199m, Stock=60, CategoryID=1},

                new(){Id=2, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, Name="Atelier Rebul", Description="Bereket Çubuklu Oda Kokusu 200ml", Price=1249m, Stock=5, CategoryID=1},

                new(){Id=3,CreatedDate=DateTime.Now, Status=DataStatus.Inserted, Name="Dr. Vranjes Firenze", Description="Leather Oud 100 ml Difüzör", Price=1199m, Stock=30, CategoryID=2},

                new(){Id=4,CreatedDate=DateTime.Now, Status=DataStatus.Inserted, Name="Dr. Vranjes Firenze", Description="Ginger Lime 2500 ml Difüzör", Price=8950m, Stock=80, CategoryID=2},

                new(){Id=5,CreatedDate=DateTime.Now, Status=DataStatus.Inserted, Name="Etro", Description="Penelope Refill 500 ml Oda Kokusu", Price=1249m, Stock=43, CategoryID=3},

                new(){Id=6,CreatedDate=DateTime.Now, Status=DataStatus.Inserted, Name="Etro", Description="Eos 100 ml Oda Kokusu", Price=1449m, Stock=100, CategoryID=3},

                new(){Id=7,CreatedDate=DateTime.Now, Status=DataStatus.Inserted, Name="Lagom Candle", Description="Bubble Mor Pembe ve Su Yeşili 3'lü Mum Seti", Price=1280m, Stock=8, CategoryID=1},

                new(){Id=8,CreatedDate=DateTime.Now, Status=DataStatus.Inserted, Name="Maison Francis Kurkdjian", Description="Baccarat Rouge 540 Kokulu Mum 280 gr", Price=4590m, Stock=24, CategoryID=1},

                new(){Id=9,CreatedDate=DateTime.Now, Status=DataStatus.Inserted, Name="Atelier Rebul", Description="İstanbul Çubuklu Oda Kokusu 2500ml", Price=10250m, Stock=18, CategoryID=1},
            };

            builder.HasData(products);
        }
    }
}
