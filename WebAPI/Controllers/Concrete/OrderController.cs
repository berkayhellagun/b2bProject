using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Abstract;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : GenericController<Order>
    {
        IOrderService _orderService;

        public OrderController(IOrderService orderService) : base(orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("getorderdetail")]
        public IActionResult GetOrderDetail()
        {
            var result = _orderService.GetOrderDetail();
            return result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpGet("getorderdetailbyid")]
        public IActionResult GetOrderDetail(int orderId)
        {
            var result = _orderService.GetOrderDetailById(orderId);
            return result.Data != null
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }
    }
}
