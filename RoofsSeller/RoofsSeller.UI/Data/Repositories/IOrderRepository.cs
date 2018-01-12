using System.Threading.Tasks;
using RoofsSeller.Model.Entities;
using System.Collections.Generic;

namespace RoofsSeller.UI.Data.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Product>> GetAllProductsAsync();
        Task ReloadProductAsync(int productId);
        Task<List<OrderItem>> GetAllOrderItemsAsync();
        Task<Order> GetLastOrderAsync();
        void RemoveOrderItem(OrderItem model);
    }
}