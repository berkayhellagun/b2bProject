using Business.Abstract;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<TEntity> : ControllerBase, IGenericController<TEntity>
        where TEntity : class, IEntity, new()
    {
        IGenericService<TEntity> _genericService;

        public GenericController(IGenericService<TEntity> genericService)
        {
            _genericService = genericService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(TEntity t)
        {
            var result = await _genericService.AsyncAdd(t);
            return result.Success
                ? Ok()
                : BadRequest(result.Message);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _genericService.AsyncGetAll();
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _genericService.AsyncGetById(id);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove(TEntity t)
        {
            var result = await _genericService.AsyncRemove(t);
            return result.Success
                ? Ok(result.Success)
                : BadRequest(result.Message);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(TEntity t)
        {
            var result = await _genericService.AsyncUpdate(t);
            return result.Success
                ? Ok(result.Success)
                : BadRequest(result.Message);
        }
    }
}
