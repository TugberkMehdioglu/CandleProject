using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Address : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Neighborhood { get; set; } = null!;
        public string Street { get; set; } = null!;
        public ushort AptNo { get; set; }
        public byte? Flat { get; set; }//For private property
        public string FullAddress 
        { 
            get 
            {
                return $"{Neighborhood} Mah. {Street} Sok. No:{AptNo} {(Flat.HasValue ? $"Daire:{Flat}" : "")}\n\n {City.ToUpper()} / {Country.ToUpper()}";
            }
        } //Will not be in database.


        public string AppUserProfileID { get; set; } = null!;


        //Navigation Properties
        public AppUserProfile AppUserProfile { get; set; } = null!;
        public ICollection<Order>? Orders { get; set; }
    }
}
