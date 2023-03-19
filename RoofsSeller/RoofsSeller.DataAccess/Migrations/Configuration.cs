using RoofsSeller.DataAccess.DataGenerators;

namespace RoofsSeller.DataAccess.Migrations
{
    using Model.Entities;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<RoofsSellerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RoofsSellerDbContext context)
        {
            // OrderStates
            context.OrderStates.AddOrUpdate(
                os => os.State,
                new OrderState { State = Model.Enums.OrderState.Ordered.ToString() },
                new OrderState { State = Model.Enums.OrderState.Paid.ToString() },
                new OrderState { State = Model.Enums.OrderState.Shipped.ToString() }
            );

            // ProductTypes
            context.ProductTypes.AddOrUpdate(
                pt => pt.Type,
                new ProductType { Type = Model.Enums.ProductType.ModularMetalTiles.ToString() },
                new ProductType { Type = Model.Enums.ProductType.GutterSystems.ToString() },
                new ProductType { Type = Model.Enums.ProductType.AdditionalElements.ToString() },
                new ProductType { Type = Model.Enums.ProductType.RoofMembranes.ToString() },
                new ProductType { Type = Model.Enums.ProductType.Accessories.ToString() },
                new ProductType { Type = Model.Enums.ProductType.Insulation.ToString() },
                new ProductType { Type = Model.Enums.ProductType.RoofSafetyElements.ToString() },
                new ProductType { Type = Model.Enums.ProductType.MountingElements.ToString() },
                new ProductType { Type = Model.Enums.ProductType.FlatMetalSheets.ToString() },
                new ProductType { Type = Model.Enums.ProductType.Other.ToString() }
            );

            // ProductDiscounts
            context.ProductDiscounts.AddOrUpdate(
                dr=>dr.Rate,
                new ProductDiscount { Rate = 0 },
                new ProductDiscount { Rate = 5 },
                new ProductDiscount { Rate = 10 },
                new ProductDiscount { Rate = 15 },
                new ProductDiscount { Rate = 20 },
                new ProductDiscount { Rate = 25 },
                new ProductDiscount { Rate = 30 },
                new ProductDiscount { Rate = 35 },
                new ProductDiscount { Rate = 40 },
                new ProductDiscount { Rate = 45 },
                new ProductDiscount { Rate = 50 }
            );

            // ProductMeasures
            context.ProductMeasures.AddOrUpdate(
                pm => pm.Measure,
                new ProductMeasure { Measure = "units" },
                new ProductMeasure { Measure = "sq.m." },
                new ProductMeasure { Measure = "cu.m." },
                new ProductMeasure { Measure = "run.m." },
                new ProductMeasure { Measure = "kg" },
                new ProductMeasure { Measure = "ton" }
            );

            // Customers
            var customerGenerator = FakeDataGenerator.GetCustomerGenerator();
            //var customers = customerGenerator.Generate(10);
            context.Customers.AddOrUpdate(c => c.Name, 
                customerGenerator.Generate(),
                customerGenerator.Generate(),
                customerGenerator.Generate(),
                customerGenerator.Generate(),
                customerGenerator.Generate());

            context.SaveChanges();

            // Orders
            context.Orders.AddOrUpdate(
                o => o.OrderNumber,
                new Order
                {
                    OrderNumber = 100000001,
                    OrderDate = new System.DateTime(2017, 05, 21),
                    ShippingDate = new System.DateTime(2017, 05, 23),
                    TotalAmount = 993.75m,
                    StateId = 3,
                    CustomerId = 1
                },
                new Order
                {
                    OrderNumber = 100000002,
                    OrderDate = new System.DateTime(2017, 05, 22),
                    ShippingDate = new System.DateTime(2017, 05, 24),
                    TotalAmount = 3517.88m,
                    StateId = 3,
                    CustomerId = 2
                },
                new Order
                {
                    OrderNumber = 100000003,
                    OrderDate = new System.DateTime(2017, 05, 22),
                    ShippingDate = new System.DateTime(2017, 05, 25),
                    TotalAmount = 1102.24m,
                    StateId = 3,
                    CustomerId = 3
                },
                new Order
                {
                    OrderNumber = 100000004,
                    OrderDate = new System.DateTime(2017, 05, 22),
                    ShippingDate = new System.DateTime(2017, 05, 22),
                    TotalAmount = 935.08m,
                    StateId = 3,
                    CustomerId = 4
                },
                new Order
                {
                    OrderNumber = 100000005,
                    OrderDate = new System.DateTime(2017, 05, 23),
                    ShippingDate = new System.DateTime(2017, 05, 23),
                    TotalAmount = 1315.09m,
                    StateId = 3,
                    CustomerId = 5
                });

            context.SaveChanges();

            // Providers/Products
            var productGenerator = FakeDataGenerator.GetProductGenerator();
            var providerGenerator = FakeDataGenerator.GetProviderGenerator();
            var providers = providerGenerator.Generate(6);
            var productsCounts = new[] { 8, 4, 2, 3, 2, 2};
            for (var i = 0; i < providers.Count; i++)
            {
                var provider = providers[i];
                provider.Products = productGenerator.Generate(productsCounts[i]);
            }

            context.Providers.AddRange(providers);
            context.SaveChanges();

            // OrderItems
            context.OrderItems.AddOrUpdate(
                new OrderItem
                {
                    OrderId = 1,
                    ProductId = 4,
                    Quantity = 120,
                    UnitPrice = 5.99m,
                    TotalPrice = 718.80m
                },
                new OrderItem
                {
                    OrderId = 1,
                    ProductId = 8,
                    Quantity = 5,
                    UnitPrice = 54.99m,
                    TotalPrice = 274.95m
                },
                new OrderItem
                {
                    OrderId = 2,
                    ProductId = 1,
                    Quantity = 200,
                    UnitPrice = 10.99m,
                    TotalPrice = 2198.00m
                },
                new OrderItem
                {
                    OrderId = 2,
                    ProductId = 10,
                    Quantity = 12,
                    UnitPrice = 109.99m,
                    TotalPrice = 1319.88m
                },
                new OrderItem
                {
                    OrderId = 3,
                    ProductId = 1,
                    Quantity = 50,
                    UnitPrice = 10.99m,
                    TotalPrice = 549.5m
                },
                new OrderItem
                {
                    OrderId = 3,
                    ProductId = 5,
                    Quantity = 10,
                    UnitPrice = 10.49m,
                    TotalPrice = 104.9m
                },
                new OrderItem
                {
                    OrderId = 3,
                    ProductId = 9,
                    Quantity = 4,
                    UnitPrice = 69.99m,
                    TotalPrice = 279.96m
                },
                new OrderItem
                {
                    OrderId = 3,
                    ProductId = 15,
                    Quantity = 12,
                    UnitPrice = 13.99m,
                    TotalPrice = 167.88m
                },
                new OrderItem
                {
                    OrderId = 4,
                    ProductId = 3,
                    Quantity = 58,
                    UnitPrice = 7.99m,
                    TotalPrice = 463.42m
                },
                new OrderItem
                {
                    OrderId = 4,
                    ProductId = 6,
                    Quantity = 8,
                    UnitPrice = 10.49m,
                    TotalPrice = 83.92m
                },
                new OrderItem
                {
                    OrderId = 4,
                    ProductId = 12,
                    Quantity = 6,
                    UnitPrice = 17.99m,
                    TotalPrice = 107.94m
                },
                new OrderItem
                {
                    OrderId = 4,
                    ProductId = 15,
                    Quantity = 10,
                    UnitPrice = 13.99m,
                    TotalPrice = 139.9m
                },
                new OrderItem
                {
                    OrderId = 4,
                    ProductId = 16,
                    Quantity = 10,
                    UnitPrice = 13.99m,
                    TotalPrice = 139.9m
                },
                new OrderItem
                {
                    OrderId = 5,
                    ProductId = 2,
                    Quantity = 70,
                    UnitPrice = 13.99m,
                    TotalPrice = 979.3m
                },
                new OrderItem
                {
                    OrderId = 5,
                    ProductId = 10,
                    Quantity = 10,
                    UnitPrice = 13.99m,
                    TotalPrice = 139.9m
                },
                new OrderItem
                {
                    OrderId = 5,
                    ProductId = 14,
                    Quantity = 1,
                    UnitPrice = 55.99m,
                    TotalPrice = 55.99m
                },
                new OrderItem
                {
                    OrderId = 5,
                    ProductId = 16,
                    Quantity = 10,
                    UnitPrice = 13.99m,
                    TotalPrice = 139.9m
                });
        }
    }
}