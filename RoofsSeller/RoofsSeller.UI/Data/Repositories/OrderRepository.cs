using RoofsSeller.DataAccess;
using RoofsSeller.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace RoofsSeller.UI.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order, RoofsSellerDbContext>, IOrderRepository
    {
        public OrderRepository(RoofsSellerDbContext context) : base(context)
        {
        }

        public async override Task<Order> GetByIdAsync(int id)
        {
            return await Context.Orders
                .Include(o => o.Customer)
                .Include(o => o.State)
                .Include(o=>o.OrderItems)
                .Include(o => o.OrderItems.Select(c => c.Product.ProductMeasure))
                .Include(o => o.OrderItems.Select(c => c.Product.ProductType))
                .Include(o => o.OrderItems.Select(c => c.Product.ProductDiscount))
                .SingleAsync(o=>o.Id == id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await Context.Set<Product>()
                .Include(p=>p.OrderItems.Select(o=>o.Order))
                .Include(p=>p.ProductDiscount)
                .Include(p=>p.ProductMeasure)
                .Include(p=>p.ProductType)
                .ToListAsync();
        }

        public async Task ReloadProductAsync(int productId)
        {
            var dbEntityEntry = Context.ChangeTracker.Entries<Product>()
                .SingleOrDefault(db => db.Entity.Id == productId);
            if (dbEntityEntry != null)
            {
                await dbEntityEntry.ReloadAsync();
            }
        }

        public async Task<List<OrderItem>> GetAllOrderItemsAsync()
        {
            return await Context.OrderItems
                .Include(o => o.Product)
                .ToListAsync();
        }

        public async Task<Order> GetLastOrderAsync()
        {
            return await Context.Orders
                .OrderByDescending(o => o.Id)
                .FirstOrDefaultAsync();
        }

        public void RemoveOrderItem(OrderItem model)
        {
            Context.OrderItems.Remove(model);
        }
    }
}
