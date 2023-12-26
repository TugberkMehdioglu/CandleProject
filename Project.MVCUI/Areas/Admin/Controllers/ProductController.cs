using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstarcts;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly IMapper _mapper;

        public ProductController(IProductManager productManager, IMapper mapper)
        {
            _productManager = productManager;
            _mapper = mapper;
        }


        [HttpGet("{search?}/{pageNumber?}/{pageSize?}")]
        public async Task<IActionResult> ProductList(string? search, int pageNumber = 1, int pageSize = 5)
        {
            IQueryable<Product> query = _productManager.GetActives();

            if (search != null)
                query.Where(x => x.Name.ToLower().Trim() == search.ToLower().Trim());


            List<ProductViewModel> productViewModels = await query.OrderBy(x => x.CreatedDate)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Include(x => x.Category).Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Stock = x.Stock,
                    ImagePath = x.ImagePath,
                    CategoryID = x.Category.Id,
                    CategoryName = x.Category.Name
                }).ToListAsync();


            int totalItemsCount = await query.CountAsync();


            ViewBag.totalPageCount = (int)Math.Ceiling((double)totalItemsCount / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.totalItemsCount = totalItemsCount;

            return View(productViewModels);
        }
    }
}
