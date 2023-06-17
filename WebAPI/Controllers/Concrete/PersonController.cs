using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : GenericController<Person>
    {
        IPersonService _userService;

        public PersonController(IPersonService userService) : base(userService)
        {
            _userService = userService;
        }

        [HttpGet("getbyemail")]
        public async Task<IActionResult> GetByMail(string email)
        {
            var result = await _userService.AsyncGetByMail(email);
            return result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }
    }
}
