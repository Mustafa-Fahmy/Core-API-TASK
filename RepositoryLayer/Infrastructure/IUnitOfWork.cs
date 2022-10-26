using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Infrastructure
{
    public interface IUnitOfWork
    {

        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        int SaveChanges();
    }
}
