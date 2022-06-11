using DESAFIOKHIPO.DesafioDotNet.Models;
using DESAFIOKHIPO.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DESAFIOKHIPO.DesafioDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsRepository _Repository;

        public ProductController(IProductsRepository pRepository)
        {
            _Repository = pRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lProducts = await _Repository.SearchProducts();
            return lProducts.Any() ? Ok(lProducts) : NoContent();
        }

        [HttpGet("{:ProductId}")]
        public async Task<IActionResult> GetById(int pProductID)
        {
            var lProduct = await _Repository.SearchProductById(pProductID);
            return lProduct != null ? Ok(lProduct) : NotFound("No product found!");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product pProduct)
        {
            _Repository.AddProduct(pProduct);
            return await _Repository.SaveChangesAsync() ? Ok("Product Saved!") : BadRequest("Error adding product!");
        }

        [HttpPut("{:ProductId}")]
        public async Task<IActionResult> Put(int pProductID, Product pProduct)
        {
            var lProductDataBase = await _Repository.SearchProductById(pProductID);

            if (pProduct == null)
                return NotFound("No product found!");

            lProductDataBase.Name = pProduct.Name ?? lProductDataBase.Name;
            lProductDataBase.Brand = pProduct.Brand ?? lProductDataBase.Brand;
            lProductDataBase.Price = pProduct.Price != lProductDataBase.Price ? pProduct.Price : lProductDataBase.Price;
            lProductDataBase.CreatedAt = lProductDataBase.CreatedAt;
            lProductDataBase.UpdatedAt = DateTime.Now;

            _Repository.RefreshProduct(lProductDataBase);

            return await _Repository.SaveChangesAsync() ? Ok("updated product!") : BadRequest("Error changing product!");
        }

        [HttpDelete("{:ProductId}")]
        public async Task<IActionResult> Delete(int pProductID)
        {
            var lProduct = await _Repository.SearchProductById(pProductID);
            if (lProduct == null)
                return NotFound("No product found!");

            _Repository.DeleteProduct(lProduct);

            return await _Repository.SaveChangesAsync() ? Ok("Deleted product!") : BadRequest("Error deleting product!");
        }
    }
}