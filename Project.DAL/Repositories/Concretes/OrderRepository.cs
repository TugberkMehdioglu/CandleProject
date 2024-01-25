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

        public IQueryable<Order> GetOrderByUserId(string userId) => _context.Orders.Where(x => x.Status != DataStatus.Deleted && x.AppUserProfileID == userId).OrderByDescending(x => x.CreatedDate).Select(x => new Order()
        {
            Id = x.Id,
            TotalPrice = x.TotalPrice,
            CreatedDate = x.CreatedDate
        });

        public async Task<Order?> GetOrderWithAddressProfileDetailProduct(int orderId) => await _context.Orders.Where(x => x.Id == orderId).Include(x => x.Address).Include(x => x.AppUserProfile).ThenInclude(x => x.AppUser).Include(x => x.OrderDetails).ThenInclude(x => x.Product).Select(x => new Order()
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
                LastName = x.AppUserProfile.LastName,
                AppUser = new AppUser()
                {
                    Email = x.AppUserProfile.AppUser.Email,
                    PhoneNumber = x.AppUserProfile.AppUser.PhoneNumber
                }
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
