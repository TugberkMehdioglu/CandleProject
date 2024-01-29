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
    public class PhotoConfiguration : BaseConfiguration<Photo>
    {
        public override void Configure(EntityTypeBuilder<Photo> builder)
        {
            base.Configure(builder);

            List<Photo> photos = new List<Photo>()
            {
                new(){Id=1, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="Dr.VranjesFirenze.jpg", ProductId = 1},
                new(){Id=2, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="AtelierRebul.jpg", ProductId = 2},
                new(){Id=3, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="LeatherOud100mlDifüzör.jpg", ProductId = 3},
                new(){Id=4, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="5pnyh3oo.g0a_IMG_01_2110095020048.jpg", ProductId = 4},
                new(){Id=5, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="qsbkczrj.lq0_IMG_01_2110089386044.jpg", ProductId = 5},
                new(){Id=6, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="kh1gwhxa.2uh_IMG_01_2110089385948.jpg", ProductId = 6},
                new(){Id=7, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="ptowbl3w.zkq_MP_1b565ca5-4923-4c99-9313-e81502939778_1_43487206954810324050030543535_563.jpg", ProductId = 7},
                new(){Id=8, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="x5p2g5gu.4fw_IMG_01_3700559608067.jpg", ProductId = 8},
                new(){Id=9, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="ihlf3u2z.vwy_IMG_01_8691226631783.jpg", ProductId = 9},
                new(){Id=10, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="rblnyed2.jvf_IMG_02_2110095020048.jpg", ProductId = 4},
                new(){Id=11, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="vb0cwnjy.qfj_IMG_03_2110095020048.jpg", ProductId = 4},
                new(){Id=12, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="0fbp5mpz.kbk_IMG_05_8691226631783.jpg", ProductId = 9},
                new(){Id=13, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="q5hq1lxg.y3y_IMG_03_8691226631783.jpg", ProductId = 9},
                new(){Id=14, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="3334.jpg", ProductId = 3},
                new(){Id=15, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="fpzkb4bw.di1_IMG_03_3700559608067.jpg", ProductId = 8},
                new(){Id=16, CreatedDate=DateTime.Now, Status=DataStatus.Inserted, ImagePath="iz2behpe.ci3_IMG_02_3700559608067.jpg", ProductId = 8}
            };

            builder.HasData(photos);
        }
    }
}
