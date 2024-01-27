using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Abstracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        public IQueryable<Order> GetOrdersWithProfiles();
        public IQueryable<Order> GetOrderByUserId(string userId);
        public Task<Order?> GetOrderWithAddressProfileDetailProduct(int orderId);
        public Task<Order?> GetOrderViaUserIdWithAddressProfileDetailProduct(int orderId, string userId);
    }
}
