using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminViewModels;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> UserList()
        {
            List<UserViewModel> userViewModels = await _userManager.Users.Where(x => x.Status != DataStatus.Deleted).Select(x => new UserViewModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                EmailConfirmed = x.EmailConfirmed
            }).ToListAsync();

            foreach (UserViewModel user in userViewModels)
            {
                user.Roles = await _userManager.GetRolesAsync(new AppUser() { Id = user.Id });
            }

            return View(userViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AssignRoleToUser(string id)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(id);
            if(appUser == null)
            {
                TempData["fail"] = "Kullanıcı bulunamadı";
                return RedirectToAction(nameof(UserList));
            }

            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            IList<string> userRoles = await _userManager.GetRolesAsync(appUser);

            List<AssignRoleToUserViewModel> assignRoleToUserViewModel = new();
            foreach (IdentityRole role in roles)
            {
                AssignRoleToUserViewModel roleViewModel = new()
                {
                    Id = role.Id,
                    Name = role.Name
                };

                if (userRoles.Contains(role.Name)) roleViewModel.Exist = true;

                if (role.Name == "Admin" && appUser.Id == "5c8defd5-91f2-4256-9f16-e7fa7546dec4") continue;

                assignRoleToUserViewModel.Add(roleViewModel);
            }

            TempData["Id"] = appUser.Id;

            return View(assignRoleToUserViewModel);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRoleToUser(List<AssignRoleToUserViewModel> request)
        {
            string userId = TempData["Id"]!.ToString()!;

            AppUser? appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                TempData["fail"] = "Kullanıcı bulunamadı";
                return RedirectToAction(nameof(UserList));
            }

            foreach (AssignRoleToUserViewModel item in request)
            {
                if (item.Exist) await _userManager.AddToRoleAsync(appUser, item.Name);
                else await _userManager.RemoveFromRoleAsync(appUser, item.Name);
            }

            TempData["success"] = "Rol atama gerçekleştirildi";
            return RedirectToAction(nameof(UserList));
        }
    }
}
