using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController: GenericController<SubCategory>
    {
        ISubCategoryService _subCategoryService;
        public SubCategoryController(ISubCategoryService subCategoryService) : base(subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }
    }
}
