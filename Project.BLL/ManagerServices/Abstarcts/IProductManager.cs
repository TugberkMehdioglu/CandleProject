﻿using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Abstarcts
{
    public interface IProductManager : IManager<Product>
    {
        public Task<(string?, Product?)> GetActiveProductWithCategory(int id);
    }
}
