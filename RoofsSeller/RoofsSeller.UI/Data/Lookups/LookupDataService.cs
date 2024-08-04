using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoofsSeller.DataAccess;
using RoofsSeller.Model;
using System.Data.Entity;

namespace RoofsSeller.UI.Data.Lookups
{
    public class LookupDataService :
        ICustomerLookupDataService, IProviderLookupDataService, 
        IProductLookupDataService, IOrderLookupDataService, 
        IProductTypeLookupDataService, IProductDiscountLookupDataService,
        IProductMeasureLookupDataService, IOrderStateLookupDataService
    {
        private Func<RoofsSellerDbContext> _contextCreator;

        public LookupDataService(Func<RoofsSellerDbContext> contextCreater)
        {
            _contextCreator = contextCreater;
        }

        public async Task<IEnumerable<LookupItem>> GetCustomerLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
               return await ctx.Customers.AsNoTracking()
                    .Select(f =>
                    new LookupItem
                    {
                        Id = f.Id,
                        DisplayMember = f.Name
                    })
                    .OrderBy(e=>e.DisplayMember)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<LookupItem>> GetOrderLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Orders.AsNoTracking()
                    .Select(p =>
                    new LookupItem
                    {
                        Id = p.Id,
                        DisplayMember = "Order #" + p.OrderNumber.ToString() + " of " + p.OrderDate.ToString()
                    })
                    .OrderBy(e => e.DisplayMember)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<LookupItem>> GetProductLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Products.AsNoTracking()
                    .Select(p =>
                    new LookupItem
                    {
                        Id = p.Id,
                        DisplayMember = p.Name
                    })
                    .OrderBy(e => e.DisplayMember)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<LookupItem>> GetProviderLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Providers.AsNoTracking()
                     .Select(f =>
                     new LookupItem
                     {
                         Id = f.Id,
                         DisplayMember = f.Name
                     })
                     .OrderBy(e => e.DisplayMember)
                     .ToListAsync();
            }
        }

        public async Task<IEnumerable<LookupItem>> GetProductTypeLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.ProductTypes.AsNoTracking()
                     .Select(f =>
                     new LookupItem
                     {
                         Id = f.Id,
                         DisplayMember = f.Type
                     })
                     .ToListAsync();
            }
        }

        public async Task<IEnumerable<LookupItem>> GetOrderStateLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.OrderStates.AsNoTracking()
                     .Select(f =>
                     new LookupItem
                     {
                         Id = f.Id,
                         DisplayMember = f.State
                     })
                     .ToListAsync();
            }
        }

        public async Task<IEnumerable<LookupItem>> GetProductDiscountLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.ProductDiscounts.AsNoTracking()
                     .Select(f =>
                     new LookupItem
                     {
                         Id = f.Id,
                         DisplayMember = f.Rate.ToString()
                     })
                     .ToListAsync();
            }
        }

        public async Task<IEnumerable<LookupItem>> GetProductMeasureLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.ProductMeasures.AsNoTracking()
                     .Select(f =>
                     new LookupItem
                     {
                         Id = f.Id,
                         DisplayMember = f.Measure
                     })
                     .ToListAsync();
            }
        }

        public async Task<IEnumerable<LookupItemExtended>> GetMetalLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Products.AsNoTracking()
                    .Where(p=>p.ProductType.Type == "Flat metal sheet")
                    .Select(p =>
                        new LookupItemExtended()
                        {
                            Id = p.Id,
                            DisplayMember = p.Name,
                            Cost = p.Price
                        })
                    .OrderBy(e => e.DisplayMember)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<LookupItemExtended>> GetRoofingLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Products.AsNoTracking()
                    .Where(p => p.ProductType.Type == "Modular metal tiles")
                    .Select(p =>
                        new LookupItemExtended()
                        {
                            Id = p.Id,
                            DisplayMember = p.Name,
                            Cost = p.Price
                        })
                    .OrderBy(e => e.DisplayMember)
                    .ToListAsync();
            }
        }
    }
}
