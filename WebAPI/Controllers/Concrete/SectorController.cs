using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : GenericController<Sector>
    {
        ISectorService _sectorService;
        public SectorController(ISectorService sectorService) : base(sectorService)
        {
            _sectorService = sectorService;
        }

        [HttpGet("getfirmgroupbysector")]
        public async Task<IActionResult> GetFirmGroupBySector()
        {
            var result = _sectorService.GetFirmGroupBySector();
            return result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpGet("getfirmgroupbysectorbysectorid")]
        public async Task<IActionResult> GetFirmGroupBySectorBySectorId(int sectorId)
        {
            var result = _sectorService.GetFirmGroupBySectorBySectorId(sectorId);
            return result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }
    }
}
