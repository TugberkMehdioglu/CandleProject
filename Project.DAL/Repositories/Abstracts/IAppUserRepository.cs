using Microsoft.AspNetCore.Identity;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Abstracts
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        new Task<IEnumerable<IdentityError>?> AddAsync(AppUser user);

        public Task<AppUser?> GetUserWithProfile(string userName);

        new Task<IEnumerable<IdentityError>?> UpdateAsync(AppUser entity);
    }
}
