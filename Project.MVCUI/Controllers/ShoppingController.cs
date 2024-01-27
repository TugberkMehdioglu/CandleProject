using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstarcts;
using Project.COMMON.Extensions;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.Areas.Member.MemberViewModels;
using Project.MVCUI.Extensions;
using Project.MVCUI.ShoppingTools;
using Project.MVCUI.ViewModels.WrapperClasses;

namespace Project.MVCUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class ShoppingController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IAppUserManager _appUserManager;
        private readonly IAddressManager _addressManager;
        private readonly IMapper _mapper;
        private readonly IOrderManager _orderManager;
        private readonly IOrderDetailManager _orderDetailManager;

        public ShoppingController(IProductManager productManager, ICategoryManager categoryManager, IAppUserManager appUserManager, IAddressManager addressManager, IMapper mapper, IOrderManager orderManager, IOrderDetailManager orderDetailManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _appUserManager = appUserManager;
            _addressManager = addressManager;
            _mapper = mapper;
            _orderManager = orderManager;
            _orderDetailManager = orderDetailManager;
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
            else if (!string.IsNullOrEmpty(search))
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

            if (productViewModel == null)
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

        [Authorize]
        public async Task<IActionResult> ConfirmOrder()
        {
            string userName = User.Identity!.Name!;

            var (error, appUser) = await _appUserManager.GetUserWithProfileAndAddressesAsync(userName);
            if (error != null)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(CartPage));
            }

            if (!await _addressManager.AnyAsync(x => x.AppUserProfileID == appUser!.AppUserProfile!.Id && x.Status != DataStatus.Deleted))
            {
                TempData["fail"] = "Sipariş verebilmeniz için en az bir tane adres girmiş olmalısınız";
                return RedirectToAction(nameof(CartPage));
            }

            OrderWrapper wrapper = new() { AppUser = _mapper.Map<ViewModels.AppUserViewModel>(appUser) };

            return View(wrapper);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmOrder(OrderWrapper request)
        {
            ModelState.Remove("FullAddress");
            ModelState.Remove("AppUser.Password");
            ModelState.Remove("AppUser.UserName");
            ModelState.Remove("AppUser.PhoneNumber");
            ModelState.Remove("AppUser.PasswordConfirm");
            if (!ModelState.IsValid) return View(request);

            Cart? cart = HttpContext.Session.GetSession<Cart>("cart");
            if (cart == null)
            {
                TempData["fail"] = "Sepetinizde ürün bulunamadı";
                return RedirectToAction(nameof(ShoppingList));
            }

            foreach (CartItem item in cart.Basket)
            {
                short productStock = await _productManager.Where(x => x.Id == item.Id && x.Status != DataStatus.Deleted).Select(x => x.Stock).FirstOrDefaultAsync();

                int result = productStock - item.Amount;

                if(result < 0)
                {
                    TempData["fail"] = "Stok aşımı yaşandığı için sipariş alınmadı";
                    return RedirectToAction(nameof(ShoppingList));
                }
            }

            Order order = new()
            {
                TotalPrice = cart.TotalPrice,
                AppUserProfileID = request.AppUser.Id,
                AddressID = request.AddressId
            };

            string? error = await _orderManager.AddAsync(order);
            if (error != null)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(ShoppingList));
            }

            List<string> orderedProductsForMailBody = new List<string>();
            List<string> productForAlertMailBody = new List<string>();

            foreach (CartItem item in cart.Basket)
            {
                OrderDetail orderDetail = new()
                {
                    OrderID = order.Id,
                    ProductID = item.Id,
                    Quentity = item.Amount,
                    SubTotal = item.SubTotal
                };

                orderedProductsForMailBody.Add(
                    @$"
                    <li style='margin-bottom: 20px; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
                    <strong>Ürün:</strong> {item.Name}
                    <br>
                    <strong>Ürün Açıklaması:</strong> {item.Description}
                    <br>
                    <strong>Fiyat:</strong> {item.Price.ToString("C2")}
                    <br>
                    <strong>Adet:</strong> {item.Amount}
                    </li>
                    ");

                string? odError = await _orderDetailManager.AddWithOutSaveAsync(orderDetail);
                if (odError != null)
                {
                    await _orderManager.DeleteAsync(order);

                    TempData["fail"] = odError;
                    return RedirectToAction(nameof(ShoppingList));
                }

                Product? product = await _productManager.FindAsync(item.Id);
                if (product == null)
                {
                    await _orderManager.DeleteAsync(order);

                    TempData["fail"] = "Ürün bulunamadı";
                    return RedirectToAction(nameof(ShoppingList));
                }

                product!.Stock -= item.Amount;

                if (product.Stock < 5)
                {
                    productForAlertMailBody.Add(
                        $@"
                            <li style='margin-bottom: 20px; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px; word-wrap: break-word;'>
                                <strong>Ürün Id:</strong> {product.Id}
                                <br>
                                <strong>Ürün:</strong> {product.Name}
                                <br>
                                <strong>Ürün Açıklaması:</strong> {product.Description}
                                <br>
                                <strong>Kalan Adet:</strong> <span style=""color: red; font-weight: bold;"">{product.Stock}</span>
                            </li>
                        ");
                }

                string? pError = await _productManager.UpdateWithOutSaveAsync(product);
                if (pError != null)
                {
                    await _orderManager.DeleteAsync(order);

                    TempData["fail"] = pError;
                    return RedirectToAction(nameof(ShoppingList));
                }
            }

            await _orderDetailManager.SaveChangesAsync();

            if(productForAlertMailBody.Count > 0)
            {
                string orginizedProductsForAlertMailBody = string.Join(null, productForAlertMailBody);

                string alertMailBody = $@"
                    <div style='display: flex; justify-content: center; align-items: center;'>
        <div style='max-width: 800px; margin-top: 1%; padding: 20px; background-color: white; box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1); border-radius: 10px;'>
           <div style='background-color: rgb(128, 208, 199); color: white; text-align: center; padding: 15px; border-radius: 10px 10px 10px 10px; font-family: sans-serif;'>
               <h1>Candle Project</h1>
           </div>
           <div style='margin-top: 20px; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>

               <h2 style='text-align: center;'>Stok Uyarısı</h2>
               <ul style='list-style-type: none; padding: 0; margin: 0;'>
                   
                {orginizedProductsForAlertMailBody}

               </ul>
           </div>
        </div>
   </div>
                ";

                await MailService.SendMailAsync("tugberkmehdioglu@gmail.com", alertMailBody, "Stok Uyarısı | Candle Project");
            }

            //string.Concat'de olur.
            string organizedProductsForMailBody = string.Join(null, orderedProductsForMailBody);

            string? fullAddress = (await _addressManager.Where(x => x.Id == request.AddressId && x.Status != DataStatus.Deleted).Select(x => x.FullAddress).FirstOrDefaultAsync())!;

            string mailBody =
                @$"
                <div style='display: flex; justify-content: center; align-items: center;'>
             <div style='max-width: 800px; margin-top: 1%; padding: 20px; background-color: white; box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1); border-radius: 10px;'>
                <div style='background-color: rgb(128, 208, 199); color: white; text-align: center; padding: 15px; border-radius: 10px 10px 10px 10px; font-family: sans-serif;'>
                    <h1>Candle Project</h1>
                </div>
                <div style='margin-top: 20px; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
                  <div style='margin-bottom: 20px; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
                    <h2 style='text-align: center;'>Fatura Bilgileri</h2>
                    <p><strong>Müşteri Adı:</strong> {request.AppUser.AppUserProfile!.FullName}</p>
                    <p><strong>Sipariş No:</strong> {order.Id}</p>
                    <p><strong>Adres:</strong> {fullAddress}</p>
                    <p><strong>Telefon:</strong> {request.AppUser.PhoneNumber}</p>
                  </div>

                    <h2 style='text-align: center;'>Sipariş Detayları</h2>
                    <ul style='list-style-type: none; padding: 0; margin: 0;'>
                        {organizedProductsForMailBody}
                    </ul>
                    <p style='margin-top: 20px; text-align: right; font-size: 20px;'><strong>Toplam Tutar:</strong> {order.TotalPrice.ToString("C2")}</p>
                </div>
             </div>
        </div>";

            HttpContext.Session.Remove("cart");
            await MailService.SendMailAsync(request.AppUser.Email, mailBody, "Fatura Bilgisi | Candle Project");

            TempData["success"] = "Siparişiniz için teşekkürler, faturanız mail adresinize gönderildi";
            return RedirectToAction(nameof(ShoppingList));
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

            if (!basket.Basket.Any())
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
