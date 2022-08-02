using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : GenericController<OperationClaim>
    {
        IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService) : base(operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }
    }
}
