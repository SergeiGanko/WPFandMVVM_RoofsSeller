namespace RoofsSeller.DataAccess.Migrations
{
    using RoofsSeller.Model.Entities;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;

    internal sealed class Configuration : 
        DbMigrationsConfiguration<RoofsSellerDbContext>
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
                new OrderState { State = "Заказан" },
                new OrderState { State = "Оплачен" },
                new OrderState { State = "Отгружен" }
            );

            // ProductTypes
            context.ProductTypes.AddOrUpdate(
                pt=>pt.Type,
                new ProductType { Type="Модульная металлочерепица"},
                new ProductType { Type="Водосточная система"},
                new ProductType { Type="Доборные элементы"},
                new ProductType { Type="Кровельные мембраны"},
                new ProductType { Type="Аксесуары для кровли"},
                new ProductType { Type="Утеплитель"},
                new ProductType { Type="Элементы безопасности кровли"},
                new ProductType { Type="Крепеж"},
                new ProductType { Type="Плоский лист"},
                new ProductType { Type="По умолчанию"}
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
                new ProductMeasure { Measure = "шт." },
                new ProductMeasure { Measure = "рул." },
                new ProductMeasure { Measure = "м.кв." },
                new ProductMeasure { Measure = "м.куб." },
                new ProductMeasure { Measure = "м.п." },
                new ProductMeasure { Measure = "кг" },
                new ProductMeasure { Measure = "т" }
            );

            // Customers
            context.Customers.AddOrUpdate(
                c => c.Name,
                new Customer
                {
                    Name = "Грищук Н. В.",
                    Address = "г. Минск, ул. Купалы 43-12",
                    Info = "паспорт МР7892301",
                    Phone = "+375297632123",
                    Email = "gr1974@tut.by"
                },
                new Customer
                {
                    Name = "ООО Технострой",
                    Address = "г. Минск, ул. Промышленная 44, офис 3",
                    Info = "УНП 123542354",
                    Phone = "+375295555757",
                    Email = "texxstr@info.com"
                },
                new Customer
                {
                    Name = "ЧУП МобилТоргСтройИнвест",
                    Address = "г. Минск, ул. Авангардная 53",
                    Info = "УПН 123089432",
                    Phone = "+375298875432",
                    Email = "mobil1@info.com"
                },
                new Customer
                {
                    Name = "Васин В. В.",
                    Address = "г. Заславль, ул. Советская 13-66",
                    Info = "паспорт МР6667754",
                    Phone = "+375447651313",
                    Email = "vasya1980@tut.by"
                },
                new Customer
                {
                    Name = "ЧУП Альтеза",
                    Address = "г. Минск, ул. Скорины 54-12",
                    Info = "УНП 103183472",
                    Phone = "+375295553344",
                    Email = "alteza@info.com"
                });

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
            context.Providers.AddRange(new Provider[]
            {
                new Provider { Name ="BUDMAT BOGDAN WIECEK",
                              Info="УНП 1119060956",
                              Address="ул. Отолинска 25, г. Плоцк, Польша",
                              Phone="+48 502 197-197",
                              Email="contact@budmat.com",
                              Products=new List<Product>
                              {
                                  new Product{ Name="BUDMAT Venecija",
                                               ProductMeasureId = 1,
                                               Price=10.99m,
                                               StockBalance=1240,
                                               ProductTypeId = 1,
                                               Info = "Высота волны 35 мм,\nРазмер модуля 750мм*1190 мм,\nПолезный размер модуля 700мм*1150 мм,\nЭффективная площадь модуля 0,805 м.кв",
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="BUDMAT Ferrara",
                                               ProductMeasureId = 1,
                                               Price=13.99m,
                                               StockBalance=542,
                                               ProductTypeId = 1,
                                               Info = "Высота волны 35 мм,\nРазмер модуля 845мм*1190 мм,\nПолезный размер модуля 800мм*1133 мм,\nЭффективная площадь модуля 0,91 м.кв",
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="BUDMAT Murano",
                                               ProductMeasureId = 1,
                                               Price=7.99m,
                                               StockBalance=746,
                                               ProductTypeId = 1,
                                               Info = "Высота волны 35 мм,\nРазмер модуля 725мм*1196 мм,\nПолезный размер модуля 700мм*1150 мм,\nЭффективная площадь модуля 0,805 м.кв",
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="BUDMAT Rialto",
                                               ProductMeasureId = 1,
                                               Price=5.99m,
                                               StockBalance=920,
                                               ProductTypeId = 1,
                                               Info = "Высота волны 35 мм,\nРазмер модуля 735мм*1195 мм,\nПолезный размер модуля 700мм*1150 мм,\nЭффективная площадь модуля 0,805 м.кв",
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="Водосточный желоб 2 м.",
                                               ProductMeasureId = 1,
                                               Price=5.49m,
                                               StockBalance=680,
                                               ProductTypeId = 2,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="Водосточный желоб 4 м.",
                                               ProductMeasureId = 1,
                                               Price=10.49m,
                                               StockBalance=680,
                                               ProductTypeId = 2,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="Соединитель желоба",
                                               ProductMeasureId = 1,
                                               Price=3.49m,
                                               StockBalance=780,
                                               ProductTypeId = 2,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="BUDMAT Bella Sara",
                                               ProductMeasureId = 1,
                                               Price=10.99m,
                                               StockBalance=1000,
                                               ProductTypeId = 1,
                                               Info = "Высота волны 35 мм,\nРазмер модуля 735мм*1180 мм,\nПолезный размер модуля 700мм*1100 мм,\nЭффективная площадь модуля 0,77 м.кв",
                                               ProductDiscountId = 1
                                  }
                              }
                },
                new Provider { Name ="CB S.A.",
                              Info="УНП 8043201693",
                              Address="Озимска 2, г. Ополе, Польша",
                              Phone="+48 501 396-113",
                              Email="info@corotop.com.pl",
                              Products=new List<Product>
                              {
                                  new Product{ Name="Гидро-мембрана Corotop Smart 115",
                                               ProductMeasureId = 2,
                                               Price=54.99m,
                                               StockBalance=60,
                                               ProductTypeId = 4,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="Гидро-мембрана Corotop Classic 130",
                                               ProductMeasureId = 2,
                                               Price=69.99m,
                                               StockBalance=40,
                                               ProductTypeId = 4,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="Гидро-мембрана Corotop Strong 160",
                                               ProductMeasureId = 2,
                                               Price=109.99m,
                                               StockBalance=20,
                                               ProductTypeId = 4,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="Гидро-мембрана Corotop Power 250+",
                                               ProductMeasureId = 2,
                                               Price=199.99m,
                                               StockBalance=20,
                                               ProductTypeId = 4,
                                               ProductDiscountId = 1
                                  }
                              }
                },
                new Provider { Name ="ООО ХотРок",
                              Info="УНП 6503201673",
                              Address="ул. Михалковская, г. Москва, РФ",
                              Phone="+7 495 3080-494",
                              Email="info@hotrock.ru",
                              Products=new List<Product>
                              {
                                  new Product{ Name="Плита базальтовая HOTROCK Лайт Эко",
                                               ProductMeasureId = 4,
                                               Price=17.99m,
                                               StockBalance=150,
                                               ProductTypeId = 6,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="Плита базальтовая HOTROCK Фасад Про",
                                               ProductMeasureId = 4,
                                               Price=59.99m,
                                               StockBalance=80,
                                               ProductTypeId = 6,
                                               ProductDiscountId = 1
                                  }
                              }
                },
                new Provider { Name ="ООО БогеРу",
                              Info="УНП 1233268973",
                              Address="ул. Промышленная, г. Москва, РФ",
                              Phone="+7 495 1340-412",
                              Email="info@borge.ru",
                              Products=new List<Product>
                              {
                                  new Product{ Name="Комплект лестница кровельная 3 м",
                                               ProductMeasureId = 1,
                                               Price=55.99m,
                                               StockBalance=150,
                                               ProductTypeId = 7,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="Саморезы Gunnebo 35 мм, 1уп=250шт",
                                               ProductMeasureId = 1,
                                               Price=13.99m,
                                               StockBalance=350,
                                               ProductTypeId = 8,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="Саморезы коньковые 70 мм, 1уп=100шт",
                                               ProductMeasureId = 1,
                                               Price=13.99m,
                                               StockBalance=400,
                                               ProductTypeId = 8,
                                               ProductDiscountId = 1
                                  }
                              }
                },
                new Provider { Name ="ArcelorMittal",
                              Address="ул. Садовая, г. Москва, РФ",
                              Phone="+7 495 1212-112",
                              Email="info@arcelor.com",
                              Products=new List<Product>
                              {
                                  new Product{ Name="Dmatt 1,25x2",
                                               ProductMeasureId = 1,
                                               Price=9m,
                                               StockBalance=150,
                                               ProductTypeId = 9,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="S-Pure 1,25x2",
                                               ProductMeasureId = 1,
                                               Price=8m,
                                               StockBalance=350,
                                               ProductTypeId = 9,
                                               ProductDiscountId = 1
                                  }
                              }
                },
                new Provider { Name ="SSAB AB",
                              Address="Klarabergsviadukten 70, Stockholm, Sweden",
                              Phone="+375 29 345-55-11",
                              Email="info@ssab.com",
                              Products=new List<Product>
                              {
                                  new Product{ Name="Xmatt 1,25x2",
                                               ProductMeasureId = 1,
                                               Price=11m,
                                               StockBalance=100,
                                               ProductTypeId = 9,
                                               ProductDiscountId = 1
                                  }
                              }
                }
            });

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
