using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Data.Repositories
{
    public interface IProviderRepository : IGenericRepository<Provider>
    {
        void RemoveProduct(Product model);
    }
}