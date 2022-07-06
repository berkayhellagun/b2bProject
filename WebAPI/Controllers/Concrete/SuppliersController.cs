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
    }
}
