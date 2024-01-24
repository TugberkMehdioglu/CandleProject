﻿using Project.ENTITIES.Models;
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
        public Task<Order?> GetOrderWithAddressProfileDetailProduct(int orderId);
    }
}
