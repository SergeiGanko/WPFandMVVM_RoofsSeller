using System.Collections.Generic;
using System.Threading.Tasks;
using RoofsSeller.DataAccess;
using RoofsSeller.Model.Entities;
using System.Data.Entity;

namespace RoofsSeller.UI.Data.Repositories
{
    public class ProductTypeRepository : 
        GenericRepository<ProductType, RoofsSellerDbContext>, IProductTypeRepository
    {
        public ProductTypeRepository(RoofsSellerDbContext context) : base(context)
        {
        }

        public async Task<bool> IsReferencedByProductAsync(int productTypeId)
        {
            return await Context.Products.AsNoTracking()
                .AnyAsync(p => p.ProductTypeId == productTypeId);
        }

        public async Task<List<ProductType>> GetAllProductTypesAsync()
        {
            return await Context.ProductTypes.ToListAsync();
        }
    }
}
