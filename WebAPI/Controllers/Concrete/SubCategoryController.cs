using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : GenericController<SubCategory>
    {
        ISubCategoryService _subCategoryService;
        public SubCategoryController(ISubCategoryService subCategoryService) : base(subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpPost("connectCategory")]
        public async Task<IActionResult> connectCategory(int subCategoryId, int categoryId)
        {
            var result = await _subCategoryService.connectCategory(subCategoryId, categoryId);
            return result.Success
                ? Ok()
                : BadRequest(result.Message);
        }
    }
}
