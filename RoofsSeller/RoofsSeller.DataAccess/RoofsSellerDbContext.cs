using System.Data.Entity;
using RoofsSeller.Model.Entities;

namespace RoofsSeller.DataAccess
{
    public class RoofsSellerDbContext : DbContext
    {
        public RoofsSellerDbContext() : base("RoofsSellerDb3")
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductMeasure> ProductMeasures { get; set; }
        public DbSet<Provider> Providers { get; set; }
    }
}
