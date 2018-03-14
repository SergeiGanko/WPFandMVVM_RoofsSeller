using RoofsSeller.DataAccess;
using RoofsSeller.Model.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace RoofsSeller.UI.Data.Repositories
{
    public class CustomerRepository : 
        GenericRepository<Customer, RoofsSellerDbContext>, ICustomerRepository
    {
        // ctor creating RoofsSellerDbContext instance by using Autofac
        public CustomerRepository(RoofsSellerDbContext context) :base(context)
        {
        }

        public override async Task<Customer> GetByIdAsync(int customerId)
        {
            return await Context.Customers
                .Include(c => c.Orders.Select(o => o.State))
                .SingleAsync(c => c.Id == customerId);
        }

        public void RemoveOrder(Order model)
        {
            Context.Orders.Remove(model);
        }

        public async Task<bool> HasOrdersAsync(int customerId)
        {
            return await Context.Orders.AsNoTracking()
                .Include(c => c.Customer)
                .AnyAsync(c => c.Customer.Id == customerId);
        }
    }
}
