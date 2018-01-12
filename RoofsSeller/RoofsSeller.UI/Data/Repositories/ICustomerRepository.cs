using System.Collections.Generic;
using RoofsSeller.Model.Entities;
using System.Threading.Tasks;

namespace RoofsSeller.UI.Data.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        void RemoveOrder(Order model);
        Task<bool> HasOrdersAsync(int customerId);
    }
}