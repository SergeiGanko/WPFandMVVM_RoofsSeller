using System.Collections.Generic;
using RoofsSeller.Model.Entities;
using System.Threading.Tasks;

namespace RoofsSeller.UI.Data.Repositories
{
    public interface IProductTypeRepository : IGenericRepository<ProductType>
    {
        Task<bool> IsReferencedByProductAsync(int productTypeId);
        Task<List<ProductType>> GetAllProductTypesAsync();
    }
}
