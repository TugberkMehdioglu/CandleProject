using Microsoft.AspNetCore.Identity;
using Project.BLL.ManagerServices.Abstarcts;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class AppUserManager : BaseManager<AppUser>, IAppUserManager
    {
        private readonly IAppUserRepository _appUserRepository;
        public AppUserManager(IRepository<AppUser> repository, IAppUserRepository appUserRepository) : base(repository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<(IEnumerable<IdentityError>?, string?)> AddUserByIdentityAsync(AppUser entity)
        {
            if (entity == null || entity.Status == DataStatus.Deleted || entity.PasswordHash == null || entity.Email == null || entity.PhoneNumber == null || entity.UserName == null) return (null, "Lütfen zorunlu alanları doldurun");

            IEnumerable<IdentityError>? errors = await _appUserRepository.AddAsync(entity);

            if (errors != null) return (errors, null);
            else return (null, null);
        }

        public async Task<(string?, IEnumerable<IdentityError>?)> UpdateUserByIdentityAsync(AppUser entity)
        {
            if (entity == null || entity.Status == DataStatus.Deleted) return ("Lütfen gerekli alanları doldurun", null);

            IEnumerable<IdentityError>? errors;

            try
            {
                errors = await _appUserRepository.UpdateAsync(entity);
            }
            catch (Exception exception)
            {
                return ($"Veritabanı işlemi sırasında hata oluştu, ALINAN HATA => {exception.Message}, İÇERİĞİ => {exception.InnerException}", null);
            }

            if (errors != null) return (null, errors);

            return (null, null);
        }

        public async Task<(AppUser?, string?)> GetUserWithProfile(string userName)
        {
            if (userName == null) return (null, "Kullanıcı adı boş olamaz");

            AppUser? appUser;

            try
            {
                appUser = await _appUserRepository.GetUserWithProfile(userName);
            }
            catch (Exception exception)
            {
                return (null, $"Veritabanı işlemi sırasında hata oluştu, ALINAN HATA => {exception.Message}, İÇERİĞİ => {exception.InnerException}");
            }

            if (appUser == null) return (null, "Kullanıcı bulunamadı");
            else return (appUser, null);
        }
    }
}
