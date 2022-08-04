using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : GenericController<Supplier>
    {
        ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService) : base(supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("supplierdetailbyid")]
        public IActionResult SupplierDetailById(int supplierId)
        {
            var result = _supplierService.GetSupplierDetailById(supplierId);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpGet("supplierlistbycategoryid")]
        public IActionResult SupplierListByCategoryId(int categoryId)
        {
            var result = _supplierService.GetSupplierListByCategoryId(categoryId);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }
    }
}
