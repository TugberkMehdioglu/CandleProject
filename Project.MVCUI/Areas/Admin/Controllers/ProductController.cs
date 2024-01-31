using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Project.BLL.ManagerServices.Abstarcts;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.Extensions;
using System.Globalization;
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
        private readonly ICategoryManager _categoryManager;
        private readonly IFileProvider _fileProvider;
        private readonly IPhotoManager _photoManager;

        public ProductController(IProductManager productManager, IMapper mapper, ICategoryManager categoryManager, IFileProvider fileProvider, IPhotoManager photoManager)
        {
            _productManager = productManager;
            _mapper = mapper;
            _categoryManager = categoryManager;
            _fileProvider = fileProvider;
            _photoManager = photoManager;
        }


        [HttpGet("{search?}/{categoryId?}/{productSort?}/{pageNumber?}")]
        public async Task<IActionResult> ProductList(string? search,int? categoryId, string? productSort, int pageNumber = 1)
        {
            int pageSize = 6;

            IQueryable<Product> query = _productManager.GetActives();

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.filter = search;
                query = query.Where(x => x.Name.ToLower().Trim().Contains(search.ToLower().Trim()));
                
            }
            else if (categoryId.HasValue)
            {
                ViewBag.categoryId = categoryId.Value;
                query = query.Where(x => x.CategoryID == categoryId.Value);
            }

            query = Sort(query, productSort);

            List<ProductViewModel> productViewModels = await query
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Include(x => x.Category).Include(x => x.Photos.Where(x => x.Status != DataStatus.Deleted)).Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Stock = x.Stock,
                    CategoryID = x.Category.Id,
                    CategoryName = x.Category.Name,
                    Photos = x.Photos.Select(x => new PhotoViewModel()
                    {
                        Id = x.Id,
                        ImagePath = x.ImagePath,
                        ProductId = x.ProductId
                    }).ToList()
                }).ToListAsync();


            int totalItemsCount = await query.CountAsync();


            ViewBag.totalPageCount = (int)Math.Ceiling((double)totalItemsCount / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.totalItemsCount = totalItemsCount;

            return View(productViewModels);
        }

        public IQueryable<Product> Sort(IQueryable<Product> query, string? productSort)
        {
            if (!string.IsNullOrEmpty(productSort)) ViewBag.productSort = productSort;
            else return query;

            if (productSort == "ESEU") return query.OrderByDescending(x => x.CreatedDate);
            else if (productSort == "SEYU") return query.OrderByDescending(x => x.Stock);
            else if (productSort == "SEDU") return query.OrderBy(x => x.Stock);
            else if (productSort == "IEU") return query.OrderBy(x => x.CreatedDate);
            else if (productSort == "EYTU") return query.OrderByDescending(x => x.Price);
            else if (productSort == "EDTU") return query.OrderBy(x => x.Price);
            else if (productSort == "UAAZ") return query.OrderBy(x => x.Name);
            else return query.OrderByDescending(x => x.Name);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ProductDetail(int id)
        {
            ProductViewModel? productViewModel = await _productManager.Where(x => x.Id == id && x.Status != DataStatus.Deleted).Include(x => x.Photos.Where(x => x.Status != DataStatus.Deleted)).Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                CategoryID = x.Category.Id,
                CategoryName = x.Category.Name,
                CategoryDescription = x.Category.Description,
                Photos = x.Photos.Select(x => new PhotoViewModel()
                {
                    Id = x.Id,
                    ImagePath = x.ImagePath,
                    ProductId = x.ProductId
                }).ToList()
            }).FirstOrDefaultAsync();

            if(productViewModel == null)
            {
                TempData["fail"] = "Ürün bulunamadı";
                return RedirectToAction(nameof(ProductList));
            }

            return View(productViewModel);
        }

        public async Task<IActionResult> AddProduct()
        {
            await categorySelectListForProduct();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductViewModel request)
        {
            bool isConverted = decimal.TryParse(request.PriceText, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal price);
            if (!isConverted)
            {
                ModelState.AddModelErrorWithOutKey("Doğru formatta fiyat girişi yapın");
                await categorySelectListForProduct();
                return View(request);
            }
            else if(price < 1)
            {
                ModelState.AddModelErrorWithOutKey("Fiyat 0'dan büyük olmalıdır");
                await categorySelectListForProduct();
                return View(request);
            }

            request.Price = price;

            ModelState.Remove("ImagePath");
            if (!ModelState.IsValid)
            {
                await categorySelectListForProduct();
                return View(request);
            }
            else if (await _productManager.AnyAsync(x => x.Name.ToLower().Trim() == request.Name.ToLower().Trim() && x.Status != DataStatus.Deleted))
            {
                await categorySelectListForProduct();
                ModelState.AddModelErrorWithOutKey("Oluşturmaya çalıştığınız ürün zaten bulunmaktadır");
                return View(request);
            }

            List<string> entitiesImagePaths = new List<string>();

            if (request.Images != null && request.Images.Count > 0)
            {
                foreach (IFormFile item in request.Images)
                {
                    string? result = ImageUploader.UploadImageToWwwroot(item, _fileProvider, "productPics", out string? entityImagePath);
                    if (result != null)
                    {
                        ModelState.AddModelErrorWithOutKey(result);
                        await categorySelectListForProduct();
                        return View(request);
                    }

                    entitiesImagePaths.Add(entityImagePath!);
                }
            }
            else entitiesImagePaths.Add("no-image-icon.png");

            Product toBeAdded = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CategoryID = request.CategoryID
            };


            string? error = await _productManager.AddAsync(toBeAdded);
            if(error != null)
            {
                ModelState.AddModelErrorWithOutKey(error);
                await categorySelectListForProduct();
                return View(request);
            }

            foreach (string item in entitiesImagePaths)
            {
                string? photoError = await _photoManager.AddWithOutSaveAsync(new()
                {
                    ImagePath = item,
                    ProductId = toBeAdded.Id
                });

                if (photoError != null)
                {
                    ModelState.AddModelErrorWithOutKey(photoError);
                    await categorySelectListForProduct();
                    return View(request);
                }
            }

            await _photoManager.SaveChangesAsync();

            TempData["success"] = "Ürün başarıyla oluşturuldu";
            return RedirectToAction(nameof(ProductList));
        }

        public async Task categorySelectListForProduct()
        {
            List<CategoryViewModel> categoryViewModels = await _categoryManager.GetActives().Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            TempData["categorySelectList"] = new SelectList(categoryViewModels, nameof(CategoryViewModel.Id), nameof(CategoryViewModel.Name));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var (error, product) = await _productManager.GetActiveProductWithCategory(id);
            if(error != null)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(ProductList));
            }

            ProductViewModel productViewModel = new()
            {
                Name = product!.Name,
                Description = product.Description,
                PriceText = product.Price.ToString(),
                Stock = product.Stock,
                CategoryID = product.CategoryID,
                CategoryName = product.Category.Name,
                FormerName = product.Name,
                Photos = product.Photos.Select(x => new PhotoViewModel()
                {
                    Id = x.Id,
                    ImagePath = x.ImagePath,
                    ProductId = x.ProductId
                }).ToList()
            };

            await categorySelectListForProduct();


            return View(productViewModel);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(ProductViewModel request)
        {
            ModelState.Remove("ImagePath");
            if (!ModelState.IsValid)
            {
                await categorySelectListForProduct();
                return View(request);
            }
            else if (request.FormerName!.ToLower().Trim() != request.Name.ToLower().Trim() && await _productManager.AnyAsync(x => x.Name.ToLower().Trim() == request.Name.ToLower().Trim() && x.Status != DataStatus.Deleted))
            {
                ModelState.AddModelErrorWithOutKey("Güncellemek istediğiniz ürün adı zaten mevcut");
                await categorySelectListForProduct();
                return View(request);
            }

            bool isConverted = decimal.TryParse(request.PriceText, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal price);
            if (!isConverted)
            {
                ModelState.AddModelErrorWithOutKey("Doğru formatta fiyat girişi yapın");
                await categorySelectListForProduct();
                return View(request);
            }

            List<string> productPhotos = new List<string>();

            if (request.Images != null && request.Images.Count > 0)
            {
                foreach (IFormFile item in request.Images)
                {
                    string? result = ImageUploader.UploadImageToWwwroot(item, _fileProvider, "productPics", out string? entityImagePath);
                    if (result != null)
                    {
                        ModelState.AddModelErrorWithOutKey(result);
                        await categorySelectListForProduct();
                        return View(request);
                    }

                    productPhotos.Add(entityImagePath!);
                }
            }

            Product product = new()
            {
                Id = request.Id!.Value,
                Name = request.Name,
                Description = request.Description,
                Price = price,
                Stock = request.Stock,
                CategoryID = request.CategoryID
            };


            string? error = await _productManager.UpdateAsync(product);
            if(error != null)
            {
                ModelState.AddModelErrorWithOutKey(error);
                await categorySelectListForProduct();
                return View(request);
            }

            foreach (string item in productPhotos)
            {
                string? photoError = await _photoManager.AddWithOutSaveAsync(new()
                {
                    ImagePath = item,
                    ProductId = product.Id
                });

                if (photoError != null)
                {
                    ModelState.AddModelErrorWithOutKey(photoError);
                    await categorySelectListForProduct();
                    return View(request);
                }
            }

            TempData["success"] = "Ürün başarıyla güncellendi";
            return RedirectToAction(nameof(ProductList));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Product? product = await _productManager.FindAsync(id);
            if (product == null)
            {
                TempData["fail"] = "Ürün bulunamadı";
                return RedirectToAction(nameof(ProductList));
            }

            string? error = await _productManager.DeleteAsync(product!);
            if( error != null)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(ProductList));
            }

            TempData["success"] = "Ürün silindi";
            return RedirectToAction(nameof(ProductList));
        }
    }
}
