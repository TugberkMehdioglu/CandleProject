using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstarcts;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    public class OrderController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly IMapper _mapper;

        public OrderController(IOrderManager orderManager, IMapper mapper)
        {
            _orderManager = orderManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> OrderList()
        {
            List<Order> orders = await _orderManager.GetOrdersWithProfiles();

            List<OrderViewModel> orderViewModels = _mapper.Map<List<OrderViewModel>>(orders);

            return View(orderViewModels);
        }
    }
}
