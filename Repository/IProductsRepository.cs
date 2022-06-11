using System.Threading.Tasks;
using DESAFIOKHIPO.DesafioDotNet.Models;

namespace DESAFIOKHIPO.Repository
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> SearchProducts();
        Task<Product> SearchProductById(int pProductID);
        void AddProduct(Product pProduct);
        void RefreshProduct(Product pProduct);
        void DeleteProduct(Product pProduct);
        Task<bool> SaveChangesAsync();
    }
}