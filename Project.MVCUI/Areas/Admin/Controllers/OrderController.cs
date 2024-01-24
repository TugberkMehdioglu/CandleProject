using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstarcts;
using Project.ENTITIES.Enums;
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

        [HttpGet("{customerName?}/{customerSurname?}/{orderDate?}/{orderSort?}/{orderDateSort?}/{pageNumber?}")]
        public async Task<IActionResult> OrderList(string? customerName, string? customerSurname, string? orderDate, string? orderSort, string? orderDateSort, int? pageNumber = 1)
        {
            int pageSize = 10;
            int totalOrdersCount;

            IQueryable<Order> order = _orderManager.GetActives().Include(x => x.AppUserProfile);

            if(customerName != null)
            {
                ViewBag.customerName = customerName;

                if (customerSurname != null)
                {
                    ViewBag.customerSurname = customerSurname;

                    order = order.Where(x => x.AppUserProfile!.FirstName.ToLower().Trim().Contains(customerName.ToLower().Trim()) && x.AppUserProfile.LastName.ToLower().Trim().Contains(customerSurname.ToLower().Trim()));
                }
                else order = order.Where(x => x.AppUserProfile!.FirstName.ToLower().Trim().Contains(customerName.ToLower().Trim()));
            }
            else if(customerSurname != null)
            {
                ViewBag.customerSurname = customerSurname;

                order = order.Where(x => x.AppUserProfile!.LastName.ToLower().Trim().Contains(customerSurname.ToLower().Trim()));
            }

            if (orderDate != null)
            {
                ViewBag.orderDate = orderDate;

                DateTime.TryParse(orderDate, out DateTime parsedOrderDate);

                order = order.Where(x => x.CreatedDate.Date == parsedOrderDate.Date);
            }

            order = OrderSort(order, orderSort);
            order = OrderDateSort(order, orderDateSort);

            totalOrdersCount = await order.CountAsync();

            List<Order> orders = await order.Skip((pageNumber!.Value - 1) * pageSize).Take(pageSize).Select(x => new Order()
            {
                Id = x.Id,
                TotalPrice = x.TotalPrice,
                CreatedDate = x.CreatedDate,
                AppUserProfile = new AppUserProfile()
                {
                    Id = x.AppUserProfile!.Id,
                    FirstName = x.AppUserProfile.FirstName,
                    LastName = x.AppUserProfile.LastName,
                }
            }).ToListAsync();

            List<OrderViewModel> orderViewModels = _mapper.Map<List<OrderViewModel>>(orders);

            ViewBag.totalPagesCount = (int)Math.Ceiling((double)totalOrdersCount / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.totalOrdersCount = totalOrdersCount;

            return View(orderViewModels);
        }


        public IQueryable<Order> OrderSort(IQueryable<Order> query, string? orderSort)
        {
            if (!string.IsNullOrEmpty(orderSort)) ViewBag.orderSort = orderSort;
            else return query;

            if (orderSort == "ESGS") return query.OrderByDescending(x => x.CreatedDate);
            else if (orderSort == "IGS") return query.OrderBy(x => x.CreatedDate);
            else if (orderSort == "EYTS") return query.OrderByDescending(x => x.TotalPrice);
            else if (orderSort == "EDTS") return query.OrderBy(x => x.TotalPrice);
            else if (orderSort == "MAAZ") return query.OrderBy(x => x.AppUserProfile!.FirstName);
            else return query.OrderByDescending(x => x.AppUserProfile!.FirstName);
        }

        public IQueryable<Order> OrderDateSort(IQueryable<Order> query, string? orderDateSort)
        {
            if (!string.IsNullOrEmpty(orderDateSort)) ViewBag.orderDateSort = orderDateSort;
            else return query;

            if (orderDateSort == "BGS") return query.Where(x => x.CreatedDate.Date == DateTime.Today);
            else if (orderDateSort == "DGS") return query.Where(x => x.CreatedDate.Date == DateTime.Today.AddDays(-1));
            else return query.Where(x => x.CreatedDate.Date == DateTime.Today.AddDays(-2));
        }
    }
}
