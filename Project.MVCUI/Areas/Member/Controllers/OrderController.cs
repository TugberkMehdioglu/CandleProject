using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstarcts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;

namespace Project.MVCUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member, Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    public class OrderController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly IAppUserManager _appUserManager;
        private readonly IMapper _mapper;

        public OrderController(IOrderManager orderManager, IMapper mapper, IAppUserManager appUserManager)
        {
            _orderManager = orderManager;
            _mapper = mapper;
            _appUserManager = appUserManager;
        }

        [HttpGet("{pageNumber?}")]
        public async Task<IActionResult> MyOrders(int? pageNumber = 1)
        {
            int pageSize = 10;
            int totalOrdersCount;

            string userName = User.Identity!.Name!;

            string userId = (await _appUserManager.Where(x => x.UserName == userName && x.Status != DataStatus.Deleted).Select(x => x.Id).FirstOrDefaultAsync())!;

            totalOrdersCount = await _orderManager.GetOrderByUserId(userId).CountAsync();

            List<Order> orders = await _orderManager.GetOrderByUserId(userId).Skip((pageNumber!.Value - 1) * pageSize).Take(pageSize).ToListAsync();

            List<OrderViewModel> orderViewModels = _mapper.Map<List<OrderViewModel>>(orders);

            ViewBag.totalPagesCount = (int)Math.Ceiling((double)totalOrdersCount / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.totalOrdersCount = totalOrdersCount;

            return View(orderViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> MyOrderDetail(int id)
        {
            string userName = User.Identity!.Name!;
            string userId = (await _appUserManager.Where(x => x.UserName == userName && x.Status != DataStatus.Deleted).Select(x => x.Id).FirstOrDefaultAsync())!;

            var (error, order) = await _orderManager.GetOrderViaUserIdWithAddressProfileDetailProduct(id, userId);
            if(error != null)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(MyOrders));
            }

            OrderViewModel orderViewModel = _mapper.Map<OrderViewModel>(order);

            return View(orderViewModel);
        }
    }
}
