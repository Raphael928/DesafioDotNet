using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DESAFIOKHIPO.DesafioDotNet.Data;
using DESAFIOKHIPO.DesafioDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace DESAFIOKHIPO.Repository
{
    public class ProductRepository : IProductsRepository
    {
        public ProductDBContext _Context { get; set; }
        public ProductRepository(ProductDBContext pContext)
        {
            _Context = pContext;

        }

        void IProductsRepository.AddProduct(Product pProduct)
        {
            _Context.Add(pProduct);
        }

        void IProductsRepository.DeleteProduct(Product pProduct)
        {
            _Context.Remove(pProduct);
        }

        void IProductsRepository.RefreshProduct(Product pProduct)
        {
            _Context.Update(pProduct);
        }

        async Task<Product> IProductsRepository.SearchProductById(int pProductID)
        {
            return await _Context.Products.Where(x => x.Id == pProductID).FirstOrDefaultAsync();
        }

        async Task<IEnumerable<Product>> IProductsRepository.SearchProducts()
        {
            return await _Context.Products.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _Context.SaveChangesAsync() > 0;
        }
    }
}