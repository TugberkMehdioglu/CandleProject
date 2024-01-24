using Project.BLL.ManagerServices.Abstarcts;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class OrderManager : BaseManager<Order>, IOrderManager
    {
        private readonly IOrderRepository _orderRepository;
        public OrderManager(IRepository<Order> repository, IOrderRepository orderRepository) : base(repository)
        {
            _orderRepository = orderRepository;
        }

        public IQueryable<Order> GetOrdersWithProfiles() => _orderRepository.GetOrdersWithProfiles();

        public async Task<(string?, Order?)> GetOrderWithDetailsAddressProfileProduct(int orderId)
        {
            Order? order;

            try
            {
                order = await _orderRepository.GetOrderWithDetailsAddressProfileProduct(orderId);
            }
            catch (Exception exception)
            {
                return ($"Veritabanı işlemi sırasında hata oluştu, ALINAN HATA => {exception.Message}, İÇERİĞİ => {exception.InnerException}", null);
            }

            if (order == null) return ("Sipariş bulunamadı", null);
            else return (null, order);
        }
    }
}
