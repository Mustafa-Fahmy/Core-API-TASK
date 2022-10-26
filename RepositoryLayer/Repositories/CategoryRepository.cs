using DomainLayer.Models;
using RepositoryLayer.Data;
using RepositoryLayer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {

        }

    }
}
