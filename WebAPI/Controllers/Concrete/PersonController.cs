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

        [HttpGet("authperson")]
        public async Task<IActionResult> AuthPerson(string email, string password)
        {
            var result = _userService.AuthPerson(email, password);
            return result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpPost("connectpersontoorder")]
        public async Task<IActionResult> connectPersonToOrder(int orderId, int personId)
        {
            var result = await _userService.connectPersonToOrder(orderId, personId);
            return result.Success
                ? Ok()
                : BadRequest(result.Message);
        }

        [HttpGet("getproductsbysellerid")]
        public async Task<IActionResult> GetProductsBySellerId(int sellerId)
        {
            var result = _userService.GetProductsBySellerId(sellerId);
            return result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpGet("getsellerbycategoryid")]
        public async Task<IActionResult> GetSellerByCategoryId(int categoryId)
        {
            var result = _userService.GetSellerByCategoryId(categoryId);
            return result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }
    }
}
