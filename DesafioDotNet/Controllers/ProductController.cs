using DESAFIOKHIPO.DesafioDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace DESAFIOKHIPO.DesafioDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static List<Product> Products()
        {
            return new List<Product>{
                new Product{
                    CreatedAt = new DateTime(2022, 05, 31, 00, 31, 08),
                    Name = "Incredible Plastic Pants",
                    Price =  827.00M,
                    Brand = "Hauck - Johnson",
                    UpdatedAt = new DateTime(2022, 05, 31, 02, 31, 08),
                    Id = 1
                }
            };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Products());
        }

        [HttpPost]
        public IActionResult Post(Product pProduct)
        {
            var lProducts = Products();
            lProducts.Add(pProduct);
            return Ok(lProducts);
        }
    }
}