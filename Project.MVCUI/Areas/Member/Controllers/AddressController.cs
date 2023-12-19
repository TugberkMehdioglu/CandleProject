using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.ManagerServices.Abstarcts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Member.MemberViewModels;
using Project.MVCUI.Extensions;

namespace Project.MVCUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member, Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    public class AddressController : Controller
    {
        private readonly IAddressManager _addressManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAppUserManager _appUserManager;

        public AddressController(IAddressManager addressManager, UserManager<AppUser> userManager, IMapper mapper, IAppUserManager appUserManager)
        {
            _addressManager = addressManager;
            _userManager = userManager;
            _mapper = mapper;
            _appUserManager = appUserManager;
        }

        public async Task<IActionResult> Addresses()
        {
            string userName = User.Identity!.Name!;

            AppUser? appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null) return RedirectToAction("Index", "Home");

            List<Address> addresses = await _addressManager.Where(x => x.AppUserProfileID == appUser.Id && x.Status != DataStatus.Deleted).ToListAsync();

            List<AddressViewModel> addressViewModels = _mapper.Map<List<AddressViewModel>>(addresses);

            return View(addressViewModels);
        }

        
        public IActionResult AddAddress()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAddress(AddressViewModel request)
        {
            if (!ModelState.IsValid) return View();

            string userName = User.Identity!.Name!;
            string userId = (await _appUserManager.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefaultAsync())!;

            request.AppUserProfileID = userId;

            string? error = await _addressManager.AddAsync(_mapper.Map<Address>(request));
            if(error != null)
            {
                ModelState.AddModelErrorWithOutKey(error);
                return View(request);
            }

            TempData["success"] = "Adres başarıyla oluşturuldu";
            return RedirectToAction(nameof(Addresses));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateAddress(int id)
        {
            Address? toBeUpdatedAddress = await _addressManager.FindAsync(id);
            if(toBeUpdatedAddress == null)
            {
                TempData["fail"] = "Adres bulunamadı";
                return RedirectToAction(nameof(Addresses));
            }

            return View(_mapper.Map<AddressViewModel>(toBeUpdatedAddress));
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(AddressViewModel request)
        {
            if (!ModelState.IsValid) return View(request);

            Address address = _mapper.Map<Address>(request);

            string? error = await _addressManager.UpdateAsync(address);
            if(error != null)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(Addresses));
            }

            TempData["success"] = "Adres başarıyla güncellendi";
            return RedirectToAction(nameof(Addresses));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            Address? toBeDeleteddAddress = await _addressManager.FindAsync(id);
            if (toBeDeleteddAddress == null)
            {
                TempData["fail"] = "Adres bulunamadı";
                return RedirectToAction(nameof(Addresses));
            }

            string? error = await _addressManager.DeleteAsync(toBeDeleteddAddress);
            if (error != null)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(Addresses));
            }

            TempData["success"] = "Adres başarıyla silindi";
            return RedirectToAction(nameof(Addresses));
        }
    }
}
