using Project.BLL.ManagerServices.Abstarcts;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class BaseManager<T> : IManager<T> where T : class, IEntity
    {
        private readonly IRepository<T> _repository;

        public BaseManager(IRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<string?> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<string?> AddRangeAsync(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<string?> AddRangeWithOutSaveAsync(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<string?> AddWithOutSaveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<string?> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<string?> DeleteRangeAsync(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public string? DeleteRangeWithOutSave(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public string? DeleteWithOutSave(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<string?> DestroyAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<string?> DestroyRangeAsync(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public string? DestroyRangeWithOutSave(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public string? DestroyWithOutSave(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T?> FindAsync(params object[] id)
        {
            throw new NotImplementedException();
        }

        public Task<T?> FindFirstDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> FindLastDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetActives()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetModifieds()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetPassives()
        {
            throw new NotImplementedException();
        }

        public object Select(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<X?> SelectViaDtoAsync<X>(Expression<Func<T, X>> expression) where X : class
        {
            throw new NotImplementedException();
        }

        public Task<string?> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<string?> UpdateRangeAsync(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<string?> UpdateRangeWithOutSaveAsync(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<string?> UpdateWithOutSaveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
