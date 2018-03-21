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
                new OrderState { State = "�������" },
                new OrderState { State = "�������" },
                new OrderState { State = "��������" }
            );

            // ProductTypes
            context.ProductTypes.AddOrUpdate(
                pt=>pt.Type,
                new ProductType { Type="��������� ���������������"},
                new ProductType { Type="����������� �������"},
                new ProductType { Type="�������� ��������"},
                new ProductType { Type="���������� ��������"},
                new ProductType { Type="��������� ��� ������"},
                new ProductType { Type="����������"},
                new ProductType { Type="�������� ������������ ������"},
                new ProductType { Type="������"},
                new ProductType { Type="������� ����"},
                new ProductType { Type="�� ���������"}
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
                new ProductMeasure { Measure = "��." },
                new ProductMeasure { Measure = "���." },
                new ProductMeasure { Measure = "�.��." },
                new ProductMeasure { Measure = "�.���." },
                new ProductMeasure { Measure = "�.�." },
                new ProductMeasure { Measure = "��" },
                new ProductMeasure { Measure = "�" }
            );

            // Customers
            context.Customers.AddOrUpdate(
                c => c.Name,
                new Customer
                {
                    Name = "������ �. �.",
                    Address = "�. �����, ��. ������ 43-12",
                    Info = "������� ��7892301",
                    Phone = "+375297632123",
                    Email = "gr1974@tut.by"
                },
                new Customer
                {
                    Name = "��� ����������",
                    Address = "�. �����, ��. ������������ 44, ���� 3",
                    Info = "��� 123542354",
                    Phone = "+375295555757",
                    Email = "texxstr@info.com"
                },
                new Customer
                {
                    Name = "��� ��������������������",
                    Address = "�. �����, ��. ����������� 53",
                    Info = "��� 123089432",
                    Phone = "+375298875432",
                    Email = "mobil1@info.com"
                },
                new Customer
                {
                    Name = "����� �. �.",
                    Address = "�. ��������, ��. ��������� 13-66",
                    Info = "������� ��6667754",
                    Phone = "+375447651313",
                    Email = "vasya1980@tut.by"
                },
                new Customer
                {
                    Name = "��� �������",
                    Address = "�. �����, ��. ������� 54-12",
                    Info = "��� 103183472",
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
                              Info="��� 1119060956",
                              Address="��. ��������� 25, �. �����, ������",
                              Phone="+48 502 197-197",
                              Email="contact@budmat.com",
                              Products=new List<Product>
                              {
                                  new Product{ Name="BUDMAT Venecija",
                                               ProductMeasureId = 1,
                                               Price=10.99m,
                                               StockBalance=1240,
                                               ProductTypeId = 1,
                                               Info = "������ ����� 35 ��,\n������ ������ 750��*1190 ��,\n�������� ������ ������ 700��*1150 ��,\n����������� ������� ������ 0,805 �.��",
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="BUDMAT Ferrara",
                                               ProductMeasureId = 1,
                                               Price=13.99m,
                                               StockBalance=542,
                                               ProductTypeId = 1,
                                               Info = "������ ����� 35 ��,\n������ ������ 845��*1190 ��,\n�������� ������ ������ 800��*1133 ��,\n����������� ������� ������ 0,91 �.��",
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="BUDMAT Murano",
                                               ProductMeasureId = 1,
                                               Price=7.99m,
                                               StockBalance=746,
                                               ProductTypeId = 1,
                                               Info = "������ ����� 35 ��,\n������ ������ 725��*1196 ��,\n�������� ������ ������ 700��*1150 ��,\n����������� ������� ������ 0,805 �.��",
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="BUDMAT Rialto",
                                               ProductMeasureId = 1,
                                               Price=5.99m,
                                               StockBalance=920,
                                               ProductTypeId = 1,
                                               Info = "������ ����� 35 ��,\n������ ������ 735��*1195 ��,\n�������� ������ ������ 700��*1150 ��,\n����������� ������� ������ 0,805 �.��",
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="����������� ����� 2 �.",
                                               ProductMeasureId = 1,
                                               Price=5.49m,
                                               StockBalance=680,
                                               ProductTypeId = 2,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="����������� ����� 4 �.",
                                               ProductMeasureId = 1,
                                               Price=10.49m,
                                               StockBalance=680,
                                               ProductTypeId = 2,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="����������� ������",
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
                                               Info = "������ ����� 35 ��,\n������ ������ 735��*1180 ��,\n�������� ������ ������ 700��*1100 ��,\n����������� ������� ������ 0,77 �.��",
                                               ProductDiscountId = 1
                                  }
                              }
                },
                new Provider { Name ="CB S.A.",
                              Info="��� 8043201693",
                              Address="������� 2, �. �����, ������",
                              Phone="+48 501 396-113",
                              Email="info@corotop.com.pl",
                              Products=new List<Product>
                              {
                                  new Product{ Name="�����-�������� Corotop Smart 115",
                                               ProductMeasureId = 2,
                                               Price=54.99m,
                                               StockBalance=60,
                                               ProductTypeId = 4,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="�����-�������� Corotop Classic 130",
                                               ProductMeasureId = 2,
                                               Price=69.99m,
                                               StockBalance=40,
                                               ProductTypeId = 4,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="�����-�������� Corotop Strong 160",
                                               ProductMeasureId = 2,
                                               Price=109.99m,
                                               StockBalance=20,
                                               ProductTypeId = 4,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="�����-�������� Corotop Power 250+",
                                               ProductMeasureId = 2,
                                               Price=199.99m,
                                               StockBalance=20,
                                               ProductTypeId = 4,
                                               ProductDiscountId = 1
                                  }
                              }
                },
                new Provider { Name ="��� ������",
                              Info="��� 6503201673",
                              Address="��. ������������, �. ������, ��",
                              Phone="+7 495 3080-494",
                              Email="info@hotrock.ru",
                              Products=new List<Product>
                              {
                                  new Product{ Name="����� ����������� HOTROCK ���� ���",
                                               ProductMeasureId = 4,
                                               Price=17.99m,
                                               StockBalance=150,
                                               ProductTypeId = 6,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="����� ����������� HOTROCK ����� ���",
                                               ProductMeasureId = 4,
                                               Price=59.99m,
                                               StockBalance=80,
                                               ProductTypeId = 6,
                                               ProductDiscountId = 1
                                  }
                              }
                },
                new Provider { Name ="��� ������",
                              Info="��� 1233268973",
                              Address="��. ������������, �. ������, ��",
                              Phone="+7 495 1340-412",
                              Email="info@borge.ru",
                              Products=new List<Product>
                              {
                                  new Product{ Name="�������� �������� ���������� 3 �",
                                               ProductMeasureId = 1,
                                               Price=55.99m,
                                               StockBalance=150,
                                               ProductTypeId = 7,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="�������� Gunnebo 35 ��, 1��=250��",
                                               ProductMeasureId = 1,
                                               Price=13.99m,
                                               StockBalance=350,
                                               ProductTypeId = 8,
                                               ProductDiscountId = 1
                                  },
                                  new Product{ Name="�������� ��������� 70 ��, 1��=100��",
                                               ProductMeasureId = 1,
                                               Price=13.99m,
                                               StockBalance=400,
                                               ProductTypeId = 8,
                                               ProductDiscountId = 1
                                  }
                              }
                },
                new Provider { Name ="ArcelorMittal",
                              Address="��. �������, �. ������, ��",
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
