using RoofsSeller.DataAccess;
using RoofsSeller.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace RoofsSeller.UI.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product, RoofsSellerDbContext>, IProductRepository
    {
        public ProductRepository(RoofsSellerDbContext context) : base(context)
        {
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            return await Context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductDiscount)
                .SingleAsync(p => p.Id == id);
        }

        public async Task<List<Provider>> GetAllProvidersAsync()
        {
            return await Context.Set<Provider>().ToListAsync();
        }

        public async Task ReloadProviderAsync(int providerId)
        {
            var dbEntityEntry = Context.ChangeTracker.Entries<Provider>()
                .SingleOrDefault(db => db.Entity.Id == providerId);
            if (dbEntityEntry!=null)
            {
                await dbEntityEntry.ReloadAsync();
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await Context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductMeasure)
                .ToListAsync();
        }

        public async Task<List<ProductMeasure>> GetAllMeasuresAsync()
        {
            return await Context.ProductMeasures.ToListAsync();
        }
    }
}
