using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstarcts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;
using Project.MVCUI.Extensions;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryManager categoryManager, IMapper mapper)
        {
            _categoryManager = categoryManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> CategoryList()
        {
            List<CategoryViewModel> categories = await _categoryManager.GetActives().Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();

            return View(categories);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(CategoryViewModel request)
        {
            if (!ModelState.IsValid) return View(request);

            if (await _categoryManager.AnyAsync(x => x.Name.ToLower().Trim() == request.Name.ToLower().Trim() && x.Status != DataStatus.Deleted))
            {
                ModelState.AddModelErrorWithOutKey("Oluşturmak istediğiniz kategori zaten mevcut");
                return View(request);
            }

            Category category = _mapper.Map<Category>(request);

            string? result = await _categoryManager.AddAsync(category);
            if(result != null)
            {
                ModelState.AddModelErrorWithOutKey(result);
                return View(request);
            }

            TempData["success"] = "Kategori oluşturuldu";
            return RedirectToAction(nameof(CategoryList));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            CategoryViewModel? category = await _categoryManager.Where(x => x.Id == id && x.Status != DataStatus.Deleted).Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).FirstOrDefaultAsync();

            if(category == null)
            {
                TempData["fail"] = "Kategori bulunamadı";
                return RedirectToAction(nameof(CategoryList));
            }

            return View(category);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel request)
        {
            if (!ModelState.IsValid) return View(request);

            if (await _categoryManager.AnyAsync(x => x.Name.ToLower().Trim() == request.Name.ToLower().Trim() && x.Status != DataStatus.Deleted))
            {
                ModelState.AddModelErrorWithOutKey("Güncellemek istediğiniz kategori adı zaten mevcut");
                return View(request);
            }

            Category? category = _mapper.Map<Category>(request);

            string? error = await _categoryManager.UpdateAsync(category);
            if(error != null)
            {
                ModelState.AddModelErrorWithOutKey(error);
                return View(request);
            }

            TempData["success"] = "Kategori Güncellendi";
            return RedirectToAction(nameof(CategoryList));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            Category? category = await _categoryManager.Where(x => x.Id == id && x.Status != DataStatus.Deleted).FirstOrDefaultAsync();
            if (category == null)
            {
                TempData["fail"] = "Kategori bulunamadı";
                return RedirectToAction(nameof(CategoryList));
            }

            string? result = await _categoryManager.DeleteAsync(category);
            if (result != null)
            {
                TempData["fail"] = result;
                return RedirectToAction(nameof(CategoryList));
            }

            TempData["success"] = "Kategori silindi";
            return RedirectToAction(nameof(CategoryList));
        }
    }
}
