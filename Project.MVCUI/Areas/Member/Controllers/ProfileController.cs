using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstarcts;
using Project.BLL.ManagerServices.Concretes;
using Project.ENTITIES.Models;
using MemberViewModels = Project.MVCUI.Areas.Member.MemberViewModels;
using Project.MVCUI.ViewModels;
using Project.MVCUI.ViewModels.WrapperClasses;
using Project.MVCUI.Extensions;
using Project.MVCUI.Areas.Member.MemberViewModels.WrapperClasses;
using Project.MVCUI.Areas.Member.MemberViewModels;

namespace Project.MVCUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member, Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserProfileManager _appUserProfileManager;
        private readonly IAppUserManager _appUserManager;
        private readonly IMapper _mapper;

        public ProfileController(UserManager<AppUser> userManager, IAppUserProfileManager appUserProfileManager, IMapper mapper, IAppUserManager appUserManager)
        {
            _userManager = userManager;
            _appUserProfileManager = appUserProfileManager;
            _mapper = mapper;
            _appUserManager = appUserManager;
        }

        public async Task<IActionResult> Details()
        {
            string userName = User.Identity!.Name!;

            AppUser? appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null) return Redirect("/Home");

            AppUserProfile appUserProfile = (await _appUserProfileManager.FindAsync(appUser.Id))!;

            SignupViewModel wrapper = new()
            {
                AppUser = _mapper.Map<ViewModels.AppUserViewModel>(appUser),
                AppUserProfile = _mapper.Map<ViewModels.AppUserProfileViewModel>(appUserProfile)
            };

            return View(wrapper);
        }

        public async Task<IActionResult> Edit()
        {
            string userName = User.Identity!.Name!;

            var (appUser, error) = await _appUserManager.GetUserWithProfile(userName);
            if(error != null)
            {
                TempData["fail"] = error;
                return RedirectToAction(nameof(Details));
            }

            AppUserEditWrapper wrapper = new()
            {
                AppUser = _mapper.Map<MemberViewModels.AppUserViewModel>(appUser)
            };

            return View(wrapper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppUserEditWrapper request)
        {
            ModelState.Remove("AppUser.Password");
            ModelState.Remove("AppUser.PasswordConfirm");
            if (!ModelState.IsValid) return View(request);

            AppUser appUser = _mapper.Map<AppUser>(request.AppUser);
            AppUserProfile appUserProfile = _mapper.Map<AppUserProfile>(request.AppUser!.AppUserProfile);

            var (error, errors) = await _appUserManager.UpdateUserByIdentityAsync(appUser);
            if(error != null)
            {
                ModelState.AddModelErrorWithOutKey(error);
                return View(request);
            }
            else if(errors != null)
            {
                ModelState.AddModelErrorListWithOutKey(errors);
                return View(request);
            }

            string? profileError = await _appUserProfileManager.UpdateAsync(appUserProfile);
            if(profileError != null)
            {
                ModelState.AddModelErrorWithOutKey(profileError);
                return View(request);
            }

            TempData["success"] = "Profil Güncellendi";
            return RedirectToAction(nameof(Details));
        }
    }
}
