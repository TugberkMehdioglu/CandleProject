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
    public class PhotoManager : BaseManager<Photo>, IPhotoManager
    {
        public PhotoManager(IRepository<Photo> repository) : base(repository)
        {
        }


    }
}
