using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : GenericController<Property>
    {
        IPropertyService _propertyService;
        public PropertyController(IPropertyService propertyService) : base(propertyService)
        {
            _propertyService = propertyService;
        }

    }
}
