using Microsoft.EntityFrameworkCore;
using Project.DAL.ContextClasses;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Concretes
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(MyContext context) : base(context)
        {

        }

        public IQueryable<Order> GetOrdersWithProfiles() => _context.Orders.Where(x => x.Status != DataStatus.Deleted).Include(x => x.AppUserProfile).Select(x => new Order()
        {
            Id = x.Id,
            TotalPrice = x.TotalPrice,
            CreatedDate = x.CreatedDate,
            AppUserProfile = new AppUserProfile()
            {
                Id = x.AppUserProfile.Id,
                FirstName = x.AppUserProfile.FirstName,
                LastName = x.AppUserProfile.LastName,
            }
        });

        public async Task<Order?> GetOrderWithAddressProfileDetailProduct(int orderId) => await _context.Orders.Where(x => x.Id == orderId).Include(x => x.Address).Include(x => x.AppUserProfile).Include(x => x.OrderDetails).ThenInclude(x => x.Product).Select(x => new Order()
        {
            Id = x.Id,
            TotalPrice = x.TotalPrice,
            CreatedDate = x.CreatedDate,
            Address = new Address()
            {
                Id = x.Address.Id,
                Name = x.Address.Name,
                Country = x.Address.Country,
                City = x.Address.City,
                District = x.Address.District,
                Neighborhood = x.Address.Neighborhood,
                Street = x.Address.Street,
                AptNo = x.Address.AptNo,
                Flat = x.Address.Flat,
                AppUserProfileID = x.Address.AppUserProfileID
            },
            AppUserProfile = new AppUserProfile()
            {
                Id = x.AppUserProfile.Id,
                FirstName = x.AppUserProfile.FirstName,
                LastName = x.AppUserProfile.LastName
            },
            OrderDetails = x.OrderDetails.Select(x => new OrderDetail()
            {
                OrderID = x.OrderID,
                ProductID = x.ProductID,
                Quentity = x.Quentity,
                SubTotal = x.SubTotal,
                Product = new Product()
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Description = x.Product.Description,
                    Price = x.Product.Price,
                    ImagePath = x.Product.ImagePath
                }
            }).ToList()
        }).FirstOrDefaultAsync();
    }
}
