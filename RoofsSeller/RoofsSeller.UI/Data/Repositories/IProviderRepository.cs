using System.Threading.Tasks;
using RoofsSeller.Model.Entities;
using System.Collections.Generic;

namespace RoofsSeller.UI.Data.Repositories
{
    public interface IProviderRepository : IGenericRepository<Provider>
    {
        void RemoveProduct(Product model);
    }
}