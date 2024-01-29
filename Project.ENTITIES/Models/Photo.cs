using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Photo : BaseEntity
    {
        public string ImagePath { get; set; } = null!;
        public int ProductId { get; set; }


        //Navigation Properties
        public Product Product { get; set; } = null!;
    }
}
