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
    public class AppUserProfileManager : BaseManager<AppUserProfile>, IAppUserProfileManager
    {
        private readonly IAppUserProfileRepository _appUserProfileRepository;
        public AppUserProfileManager(IRepository<AppUserProfile> repository, IAppUserProfileRepository appUserProfileRepository) : base(repository)
        {
            _appUserProfileRepository = appUserProfileRepository;
        }

        public override async Task<string?> UpdateAsync(AppUserProfile entity)
        {
            if (entity == null || entity.Status == DataStatus.Deleted) return "Lütfen gerekli alanları doldurun";

            try
            {
                await _appUserProfileRepository.UpdateAsync(entity);
            }
            catch (Exception exception)
            {
                return $"Veritabanı işlemi sırasında hata oluştu, ALINAN HATA => {exception.Message}, İÇERİĞİ => {exception.InnerException}";
            }

            return null;
        }
    }
}
