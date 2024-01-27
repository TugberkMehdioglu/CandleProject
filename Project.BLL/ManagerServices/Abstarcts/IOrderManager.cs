using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Abstarcts
{
    public interface IOrderManager : IManager<Order>
    {
        public IQueryable<Order> GetOrdersWithProfiles();
        public IQueryable<Order> GetOrderByUserId(string userId);
        public Task<(string?, Order?)> GetOrderWithAddressProfileDetailProduct(int orderId);
        public Task<(string?, Order?)> GetOrderViaUserIdWithAddressProfileDetailProduct(int orderId, string userId);
    }
}
