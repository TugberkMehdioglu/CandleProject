using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstarcts;
using Project.COMMON.Extensions;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.ShoppingTools;
using Project.MVCUI.ViewModels.WrapperClasses;

namespace Project.MVCUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class ShoppingController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICategoryManager _categoryManager;

        public ShoppingController(IProductManager productManager, ICategoryManager categoryManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
        }

        [Route("/Shop")]
        [Route("/ShoppingList")]
        [HttpGet("{categoryId?}/{search?}/{selectSort?}/{pageNumber?}/{pageSize?}")]
        public async Task<IActionResult> ShoppingList(int? categoryId, string? search, string? selectSort, int pageNumber = 1, int pageSize = 6)
        {
            int totalItemsCount;

            if (categoryId.HasValue)
            {
                ViewBag.CategoryId = categoryId.Value;
                totalItemsCount = await _productManager.GetActives().Where(x => x.CategoryID == categoryId.Value).CountAsync();
            }
            else if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                totalItemsCount = await _productManager.GetActives().Where(x => x.Name.ToLower().Trim().Contains(search.ToLower().Trim())).CountAsync();
            }
            else totalItemsCount = await _productManager.GetActives().CountAsync();


            ShoppingWrapper wrapper = new ShoppingWrapper();
            IQueryable<Product> query;

            if (categoryId.HasValue)
            {
                query = _productManager.GetActives().Where(x => x.CategoryID == categoryId.Value);

                query = SortProducts(query, selectSort);

                wrapper.Products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                    Stock = x.Stock,
                    CategoryID = x.CategoryID,
                    CategoryName = x.Category.Name
                }).ToListAsync();
            }
            else if(!string.IsNullOrEmpty(search))
            {
                query = _productManager.GetActives().Where(x => x.Name.ToLower().Trim().Contains(search.ToLower().Trim()));

                query = SortProducts(query, selectSort);

                wrapper.Products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                    Stock = x.Stock,
                    CategoryID = x.CategoryID,
                    CategoryName = x.Category.Name
                }).ToListAsync();
            }
            else
            {
                query = _productManager.GetActives();

                query = SortProducts(query, selectSort);

                wrapper.Products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                    Stock = x.Stock,
                    CategoryID = x.CategoryID,
                    CategoryName = x.Category.Name
                }).ToListAsync();
            }

            wrapper.Categories = await _categoryManager.GetActives().Select(x => new CategoryViewModel() { Id = x.Id, Name = x.Name, Description = x.Description }).ToListAsync();

            ViewBag.totalPagesCount = (int)Math.Ceiling((double)totalItemsCount / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.totalItemsCount = totalItemsCount;

            return View(wrapper);
        }

        public IQueryable<Product> SortProducts(IQueryable<Product> query, string? sort)
        {
            if (!string.IsNullOrEmpty(sort)) ViewBag.selectSort = sort;
            else return query;

            if (sort == "eyu") return query.OrderBy(x => x.CreatedDate);
            else if (sort == "edf") return query.OrderBy(x => x.Price).ThenBy(x => x.CreatedDate);
            else if (sort == "eyf") return query.OrderByDescending(x => x.Price).ThenBy(x => x.CreatedDate);
            else if (sort == "aaz") return query.OrderBy(x => x.Name).ThenBy(x => x.CreatedDate);
            else return query.OrderByDescending(x => x.Name).ThenBy(x => x.CreatedDate);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ProductDetail(int id)
        {
            ProductViewModel? productViewModel = await _productManager.Where(x => x.Id == id && x.Status != DataStatus.Deleted).Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImagePath = x.ImagePath,
                Price = x.Price,
                Stock = x.Stock,
                CategoryID = x.CategoryID,
                CategoryName = x.Category.Name
            }).FirstOrDefaultAsync();

            if(productViewModel == null)
            {
                TempData["fail"] = "Ürün bulunamadı";
                return RedirectToAction(nameof(ShoppingList));
            }

            return View(productViewModel);
        }

        public IActionResult CartPage()
        {
            Cart? basket = HttpContext.Session.GetSession<Cart>("cart");

            if (basket == null)
            {
                TempData["fail"] = "Sepetinizde ürün bulunmamaktadır";
                return RedirectToAction(nameof(ShoppingList));
            }

            return View(basket);
        }

        [HttpGet("{id}/{from}/{quantity}")]
        public async Task<IActionResult> AddToCart(int id, string from, short quantity)
        {
            Product? product = await _productManager.FindAsync(id);

            if (product == null) return RedirectToAction(nameof(ShoppingList));

            Cart? basket = HttpContext.Session.GetSession<Cart>("cart");
            if (basket == null) basket = new Cart();

            CartItem cartItem = new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath,
                MaxAmount = product.Stock
            };

            if (quantity > 0 && quantity <= product.Stock) cartItem.Amount = quantity;
            else return RedirectToAction(nameof(ShoppingList));

            basket.AddToBasket(cartItem);
            HttpContext.Session.SetSession("cart", basket);

            TempData["success"] = "Ürün sepetinize eklendi";

            //There are two options for from parameter, it comes from CartPage or ProductDetail pages.
            if (from == "cart") return RedirectToAction(nameof(CartPage));
            else return RedirectToAction(nameof(ProductDetail), "Shopping", new { id });
        }

        [HttpGet("{id}/{from}")]
        public IActionResult DeleteFromCart(int id, string from)
        {
            Cart? basket = HttpContext.Session.GetSession<Cart>("cart");
            if (basket == null) return RedirectToAction(nameof(ShoppingList));

            if (!basket.Basket.Any(x => x.Id == id)) return RedirectToAction(nameof(ShoppingList));

            basket.RemoveFromBasket(id);

            if (!basket.Basket.Any())
            {
                HttpContext.Session.Remove("cart");
                TempData["fail"] = "Sepetinizde ürün bulunmamaktadır";
                return RedirectToAction(nameof(ShoppingList));
            }

            HttpContext.Session.SetSession("cart", basket);

            TempData["success"] = "Ürün sepetten silindi";

            if (from == "cart") return RedirectToAction(nameof(CartPage));
            else return RedirectToAction(nameof(ProductDetail));
        }

        [HttpGet("{id}")]
        public IActionResult DeleteProductWithAllAmountFromCart(int id)
        {
            Cart? basket = HttpContext.Session.GetSession<Cart>("cart");
            if (basket == null) return RedirectToAction(nameof(ShoppingList));

            if (!basket.Basket.Any(x => x.Id == id)) return RedirectToAction(nameof(ShoppingList));

            basket.RemoveItemWithAllAmountFromBasket(id);

            if(!basket.Basket.Any())
            {
                HttpContext.Session.Remove("cart");
                TempData["fail"] = "Sepetinizde ürün bulunmamaktadır";
                return RedirectToAction(nameof(ShoppingList));
            }

            HttpContext.Session.SetSession("cart", basket);

            TempData["success"] = "Ürün sepetten silindi";
            return RedirectToAction(nameof(CartPage));
        }
    }
}
