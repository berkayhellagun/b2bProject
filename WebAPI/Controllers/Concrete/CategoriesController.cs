using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : GenericController<Category>
    {
        ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService) : base(categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getcategorytreebyid")]
        public IActionResult GetProductWithProperties(int id)
        {
            var result = _categoryService.GetCategoryTreeById(id);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }
    }
}
