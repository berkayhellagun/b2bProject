using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : GenericController<UserOperationClaim>
    {
        IUserOperationClaimService _userOperationClaimService;
        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService) : base(userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        [HttpGet("getdetailuseroperation")]
        public IActionResult GetDetailUserOperation()
        {
            var result = _userOperationClaimService.GetDetail();
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);  
        }

        [HttpGet("getdetailbyid")]
        public IActionResult GetDetailById(int userOperationId)
        {
            var result = _userOperationClaimService.GetDetailById(userOperationId);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }
    }
}
