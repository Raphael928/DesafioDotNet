using DESAFIOKHIPO.DesafioDotNet.Models;
using DESAFIOKHIPO.DesafioDotNet.Repository;
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

        /// <summary>
        /// Retorna todos os produtos em lista.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lProducts = await _Repository.SearchProducts();
            return lProducts.Any() ? Ok(lProducts) : NoContent();
        }


        /// <summary>
        /// Retorna apenas o produto do productId.
        /// </summary>
        /// <param name="pProductID"></param>
        /// <returns></returns>
        [HttpGet("{:ProductId}")]
        public async Task<IActionResult> GetById(int pProductID)
        {
            var lProduct = await _Repository.SearchProductById(pProductID);
            return lProduct != null ? Ok(lProduct) : NotFound("No product found!");
        }

        /// <summary>
        /// Cria um produto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostNew(Product pProduct)
        {
            var lProductDataBase = new Product();

            lProductDataBase.Id = 0;
            lProductDataBase.Name = pProduct.Name;
            lProductDataBase.Brand = pProduct.Brand;
            lProductDataBase.Price = pProduct.Price;
            lProductDataBase.CreatedAt = DateTime.Now;
            lProductDataBase.UpdatedAt = DateTime.Now;

            _Repository.AddProduct(lProductDataBase);
            return await _Repository.SaveChangesAsync() ? Ok(lProductDataBase) : BadRequest("Error adding product!");
        }

        /// <summary>
        /// Edita o produto do productId.
        /// </summary>
        /// <param name="pProductID"></param>
        /// <param name="pProduct"></param>
        /// <returns></returns>
        [HttpPut]
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

            return await _Repository.SaveChangesAsync() ? Ok(lProductDataBase) : BadRequest("Error changing product!");
        }

        /// <summary>
        /// Apaga o produto do productId.
        /// </summary>
        /// <param name="pProductID"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int pProductID)
        {
            var lProduct = await _Repository.SearchProductById(pProductID);
            if (lProduct == null)
                return NotFound("No product found!");

            _Repository.DeleteProduct(lProduct);

            return await _Repository.SaveChangesAsync() ? Ok(true) : BadRequest("Error deleting product!");
        }
    }
}