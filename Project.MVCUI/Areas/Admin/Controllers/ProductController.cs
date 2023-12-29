using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstarcts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using System.Xml.Linq;

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
        public async Task<IActionResult> ProductList(string? search, int pageNumber = 1, int pageSize = 6)
        {
            IQueryable<Product> query = _productManager.GetActives();

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.filter = search;
                query = query.Where(x => x.Name.ToLower().Trim().Contains(search.ToLower().Trim()));
                
            }
                

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

        [HttpGet("{id}")]
        public async Task<IActionResult> ProductDetail(int id)
        {
            ProductViewModel? productViewModel = await _productManager.Where(x => x.Id == id && x.Status != DataStatus.Deleted).Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ImagePath = x.ImagePath,
                CategoryID = x.Category.Id,
                CategoryName = x.Category.Name,
                CategoryDescription = x.Category.Description
            }).FirstOrDefaultAsync();

            if(productViewModel == null)
            {
                TempData["fail"] = "Ürün bulunamadı";
                return RedirectToAction(nameof(ProductList));
            }

            return View(productViewModel);
        }
    }
}
