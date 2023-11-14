using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Order : BaseEntity
    {
        public int AppUserProfileID { get; set; }
        public int AddressID { get; set; }
        public decimal TotalPrice { get; set; }


        //Navigation Properties
        public Address Address { get; set; } = null!;
        public AppUserProfile AppUserProfile { get; set; } = null!;
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
