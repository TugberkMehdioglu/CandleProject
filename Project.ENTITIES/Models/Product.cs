using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public short Stock { get; set; }


        public int CategoryID { get; set; }


        //Navigation Properties
        public Category Category { get; set; } = null!;
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<Photo>? Photos { get; set; }
    }
}
