using RepositoryLayer.Data;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public IProductRepository ProductRepository { get { return new ProductRepository(_applicationDbContext); } }

        public ICategoryRepository CategoryRepository { get { return new CategoryRepository(_applicationDbContext); } }

        public int SaveChanges()
        {
           return _applicationDbContext.SaveChanges();
        }
    }
}
