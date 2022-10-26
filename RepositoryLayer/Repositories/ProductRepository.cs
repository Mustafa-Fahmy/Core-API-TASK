using DomainLayer.Models;
using RepositoryLayer.Data;
using RepositoryLayer.Infrastructure;

namespace RepositoryLayer.Repositories
{
    public interface IProductRepository : IRepository<Products>
    {

    }
    public class ProductRepository : Repository<Products>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
