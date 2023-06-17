using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : GenericController<Product>
    {
        IProductService _productService;

        public ProductsController(IProductService productService) : base(productService)
        {
            _productService = productService;
        }

        [HttpGet("getproductsmostorderedlast24hours")]
        public IActionResult GetProductsMostOrderedLast24Hours()
        {
            var result = _productService.GetProductsMostOrderedLast24Hours();
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpGet("getproductdetailbyid")]
        public IActionResult GetProductDetail(int id)
        {
            var result = _productService.GetProductDetail(id);
            return result.Success
                ? Ok(result.Data + "\nKullanılmayan Controller")
                : BadRequest(result.Message);
        }

        [HttpGet("getproductbysectorid")]
        public IActionResult GetProductsBySectorId(int sectorId)
        {
            var result = _productService.GetProductsBySectorId(sectorId);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpGet("getproductbycategoryid")]
        public IActionResult GetProductsByCategoryId(int categoryId)
        {
            var result = _productService.GetProductsBySubCategoryId(categoryId);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpGet("getproductbysubcategoryid")]
        public IActionResult GetProductsBySubCategoryId(int subCatId)
        {
            var result = _productService.GetProductsBySubCategoryId(subCatId);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpGet("getproductsdetails")]
        public IActionResult GetProductsDetails()
        {
            var result = _productService.GetProductsDetails();
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpGet("getproductdetailsbyid")]
        public IActionResult GetProductDetailsById(int productId)
        {
            var result = _productService.GetProductDetailsById(productId);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpPost("connectsubcategory")]
        public async Task<IActionResult> connectCategory(int subCategoryId, int productId)
        {
            var result = await _productService.connectSubCategory(subCategoryId, productId);
            return result.Success
                ? Ok()
                : BadRequest(result.Message);
        }

        [HttpPost("connectorder")]
        public async Task<IActionResult> connectOrder(int orderId, int productId)
        {
            var result = await _productService.connectOrder(orderId, productId);
            return result.Success
                ? Ok()
                : BadRequest(result.Message);
        }

        [HttpPost("connectproperty")]
        public async Task<IActionResult> connectProperty(int propertyId, int productId)
        {
            var result = await _productService.connectProperty(propertyId, productId);
            return result.Success
                ? Ok()
                : BadRequest(result.Message);
        }

        [HttpPost("connectseller")]
        public async Task<IActionResult> connectSeller(int sellerId, int productId)
        {
            var result = await _productService.connectSeller(sellerId, productId);
            return result.Success
                ? Ok()
                : BadRequest(result.Message);
        }
    }
}
