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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(MyContext context) : base(context)
        {

        }

        public async Task<Product?> GetActiveProductWithCategory(int id) => await _context.Products!.Where(x => x.Id == id && x.Status != DataStatus.Deleted).Include(x => x.Category).FirstOrDefaultAsync();
    }
}
