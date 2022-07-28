using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : GenericController<User>
    {
        IUserService _userService;

        public UsersController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [HttpGet("getuserbysupplierid")]
        public IActionResult GetUserBySupplierId(int supplierId)
        {
            var result = _userService.GetUserBySupplierId(supplierId);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }
    }
}
