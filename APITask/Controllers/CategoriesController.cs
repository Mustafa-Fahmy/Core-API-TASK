using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Data;
using ServiceLayer.ICustomService;
using ServiceLayer.Models;

namespace APITask.Controllers
{
    [Authorize]
    [Route("api/categories")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [ApiVersion("2.0")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductsController> _logger;
        public CategoriesController(ICategoryService customService, ILogger<ProductsController> logger)
        {
            _categoryService = customService;
            _logger = logger;

        }
        [HttpGet]
        public IActionResult GetCategories()
        {

            JsonReturn jsonReturn = _categoryService.GetCategories();
            if (!jsonReturn.success)
            {

                return NotFound(jsonReturn);
            }
            else
            {
                return Ok(jsonReturn);
            }
        }


    }
}
