using Microsoft.AspNetCore.Identity;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Abstarcts
{
    public interface IAppUserManager : IManager<AppUser>
    {
        public Task<(IEnumerable<IdentityError>?, string?)> AddUserByIdentityAsync(AppUser entity);
        public Task<(AppUser?, string?)> GetUserWithProfile(string userName);
        public Task<(string?, IEnumerable<IdentityError>?)> UpdateUserByIdentityAsync(AppUser entity);
        public Task<(IEnumerable<IdentityError>?, string?)> ChangePasswordAsync(AppUser appUser, string currentPassword, string newPassword);
    }
}
