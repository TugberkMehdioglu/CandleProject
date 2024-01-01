using Project.BLL.ManagerServices.Abstarcts;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class ProductManager : BaseManager<Product>, IProductManager
    {
        private readonly IProductRepository _productRepository;
        public ProductManager(IRepository<Product> repository, IProductRepository productRepository) : base(repository)
        {
            _productRepository = productRepository;
        }

        public async Task<(string?, Product?)> GetActiveProductWithCategory(int id)
        {
            if (id < 1) return ("Geçersiz ürün kodu", null);

            Product? product;

            try
            {
                product = await _productRepository.GetActiveProductWithCategory(id);
            }
            catch (Exception exception)
            {
                return ($"Veritabanı işlemi sırasında hata oluştu, ALINAN HATA => {exception.Message}, İÇERİĞİ => {exception.InnerException}", null);
            }

            if (product == null) return ("Ürün bulunamadı", null);

            return (null, product);
        }
    }
}
