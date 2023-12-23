using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstarcts;
using Project.BLL.ManagerServices.Concretes;
using Project.ENTITIES.Models;
using Project.MVCUI.ViewModels;
using Project.MVCUI.ViewModels.WrapperClasses;

namespace Project.MVCUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member, Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserProfileManager _appUserProfileManager;
        private readonly IMapper _mapper;

        public ProfileController(UserManager<AppUser> userManager, IAppUserProfileManager appUserProfileManager, IMapper mapper)
        {
            _userManager = userManager;
            _appUserProfileManager = appUserProfileManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Details()
        {
            string userName = User.Identity!.Name!;

            AppUser? appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null) return Redirect("/Home");

            AppUserProfile appUserProfile = (await _appUserProfileManager.FindAsync(appUser.Id))!;

            SignupViewModel wrapper = new()
            {
                AppUser = _mapper.Map<AppUserViewModel>(appUser),
                AppUserProfile = _mapper.Map<AppUserProfileViewModel>(appUserProfile)
            };

            return View(wrapper);
        }
    }
}
