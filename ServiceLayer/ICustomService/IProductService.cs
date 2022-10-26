using Microsoft.AspNetCore.JsonPatch;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomService
{
    public interface IProductService
    {
        JsonReturn GetAllProducts(PagingInputs pagingInputs);
        JsonReturn GetProductById(int id);
        JsonReturn GetProductByCategoryId(int categoryId);
        CustomJsonReturn CreateProduct(int categoryId,ProductsForCreationsDto ProductsForCreationsDto);
        JsonReturn UpdateProduct(int id, ProductsForUpdateDto productsForUpdateDto);
        bool DeleteProduct(int id);
        bool PartiallyUpdateProducts(int id, JsonPatchDocument<ProductsForUpdateDto> productsPatch);
    }
}
