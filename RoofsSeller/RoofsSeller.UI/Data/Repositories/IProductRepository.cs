using System.Threading.Tasks;
using RoofsSeller.Model.Entities;
using System.Collections.Generic;

namespace RoofsSeller.UI.Data.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Provider>> GetAllProvidersAsync();
        Task ReloadProviderAsync(int providerId);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<ProductMeasure>> GetAllMeasuresAsync();
    }
}