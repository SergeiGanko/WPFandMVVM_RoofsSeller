using RoofsSeller.DataAccess;
using RoofsSeller.Model.Entities;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace RoofsSeller.UI.Data.Repositories
{
    public class ProviderRepository : 
        GenericRepository<Provider, RoofsSellerDbContext>, IProviderRepository
    {
        public ProviderRepository(RoofsSellerDbContext context) : base(context)
        {
        }

        public async override Task<Provider> GetByIdAsync(int id)
        {
            return await Context.Providers
                .Include(p => p.Products.Select(c => c.ProductType))
                .Include(p => p.Products.Select(c => c.ProductDiscount))
                .Include(p=>p.Products.Select(c=>c.ProductMeasure))
                .SingleAsync(p => p.Id == id);
        }

        public void RemoveProduct(Product model)
        {
            Context.Products.Remove(model);
        }
    }
}
