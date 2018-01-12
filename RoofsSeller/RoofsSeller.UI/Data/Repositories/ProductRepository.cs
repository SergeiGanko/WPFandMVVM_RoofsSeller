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

        public async override Task<Product> GetByIdAsync(int id)
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
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsByProductTypeId(int productTypeId)
        {
            return await Context.Products
                .Where(p => p.ProductTypeId == productTypeId)
                .ToListAsync();
        }

        //public async Task<List<OrderItem>> GetAllSalesStatisticAsync()
        //{
        //    var result = from oi in Context.OrderItems
        //                 join p in Context.Products on oi.ProductId ==  

        //    return await Context.OrderItems
        //        .Include(o => o.Product)
        //        .ToListAsync();
        //}
    }
}
