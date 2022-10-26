using AutoMapper;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Data;
using ServiceLayer.ICustomService;
using ServiceLayer.Models;

namespace APITask.Controllers
{
    [Authorize]
    [Route("api/products")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [ApiVersion("1.0")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService customService, ILogger<ProductsController> logger)
        {
            _productService = customService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetProducts([FromQuery]PagingInputs pagingInputs)
        {

            JsonReturn jsonReturn = _productService.GetAllProducts(pagingInputs);
            if (!jsonReturn.success)
            {
                return NotFound(jsonReturn);
            }
            else
            {
                return Ok(jsonReturn);
            }
        }
        [HttpGet("{id}", Name = "GetProductById")]
        public IActionResult GetProductById(int id)
        {
            JsonReturn jsonReturn = _productService.GetProductById(id);
            if (!jsonReturn.success)
            {
                return NotFound(jsonReturn);
            }
            else
            {
                return Ok(jsonReturn);
            }
        }
        [HttpGet("[action]")]
        public IActionResult GetProductByCategoryId(int categoryID)
        {
            JsonReturn jsonReturn = _productService.GetProductById(categoryID);
            if (!jsonReturn.success)
            {
                return NotFound(jsonReturn);
            }
            else
            {
                return Ok(jsonReturn);
            }
        }
        [HttpPost("{categoryId}")]
        public IActionResult CreateProduct(int categoryId, ProductsForCreationsDto ProductsForCreationsDto)
        {
            CustomJsonReturn customeJsonReturn = _productService.CreateProduct(categoryId, ProductsForCreationsDto);
            if (!customeJsonReturn.JsonReturn.success)
            {
                return NotFound(customeJsonReturn);
            }

            return CreatedAtRoute("GetProductById", new { id = customeJsonReturn.Id }, customeJsonReturn.JsonReturn);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductsForUpdateDto productsForUpdateDto)
        {
            JsonReturn jsonReturn = _productService.UpdateProduct(id, productsForUpdateDto);
            if (!jsonReturn.success)
            {
                return NotFound(jsonReturn);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Products> DeleteProduct(int id)
        {
            bool isFound = _productService.DeleteProduct(id);
            if (!isFound)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPatch("{id}")]
        public ActionResult<Products> PartiallyUpdateProducts(int id, JsonPatchDocument<ProductsForUpdateDto> productsPatch)
        {
            bool isFound = _productService.PartiallyUpdateProducts(id, productsPatch);
            if (!isFound)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}
