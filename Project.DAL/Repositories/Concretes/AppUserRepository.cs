using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.DAL.ContextClasses;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Concretes
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AppUserRepository(MyContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public override async Task<IEnumerable<IdentityError>?> AddAsync(AppUser entity)
        {
            IdentityResult result = await _userManager.CreateAsync(entity, entity.PasswordHash);
            if (result.Succeeded!) return result.Errors;

            await _signInManager.SignInAsync(entity, true);
            return null;
        }

        public async Task<AppUser?> GetUserWithProfile(string userName) => await _context.AppUsers!.Where(x => x.UserName == userName && x.Status != DataStatus.Deleted).Include(x => x.AppUserProfile).FirstOrDefaultAsync();

        public override async Task<IEnumerable<IdentityError>?> UpdateAsync(AppUser entity)
        {
            AppUser appUser = await _userManager.FindByIdAsync(entity.Id);
            appUser.Email = entity.Email;
            appUser.UserName = entity.UserName;
            appUser.PhoneNumber = entity.PhoneNumber;
            appUser.ModifiedDate = DateTime.Now;
            appUser.Status = DataStatus.Updated;

            IdentityResult result = await _userManager.UpdateAsync(appUser);
            if (!result.Succeeded) return result.Errors;

            IdentityResult securityStampResult = await _userManager.UpdateSecurityStampAsync(appUser);
            if (!securityStampResult.Succeeded) return securityStampResult.Errors;

            await _signInManager.SignOutAsync();//Because SecurityStamp updated
            await _signInManager.SignInAsync(appUser, true);
            return null;
        }

        public async Task<IEnumerable<IdentityError>?> ChangePasswordAsync(AppUser appUser, string currentPassword, string newPassword)
        {
            IdentityResult result = await _userManager.ChangePasswordAsync(appUser, currentPassword, newPassword);
            if (!result.Succeeded) return result.Errors;

            await _userManager.UpdateSecurityStampAsync(appUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(appUser, newPassword, true, true);

            return null;
        }
    }
}
