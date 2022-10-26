using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomService
{
    public interface ICategoryService
    {
        JsonReturn GetCategories();
    }
}
