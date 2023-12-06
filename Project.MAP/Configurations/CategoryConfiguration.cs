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
    public class CategoryConfiguration : BaseConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryID);//OneToMany Relationship

            List<Category> categories = new()
            {
                new(){ Id=1, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, Name="Oda Kokusu & Mum" },
                new(){Id=2, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, Name="Difüzör"},
                new(){Id=3, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, Name="Ev Parfümü"}
            };

            builder.HasData(categories);
        }
    }
}
