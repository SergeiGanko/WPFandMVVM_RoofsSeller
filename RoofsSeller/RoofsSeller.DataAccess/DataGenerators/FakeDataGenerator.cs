using Bogus;
using Bogus.DataSets;
using RoofsSeller.Model.Entities;

namespace RoofsSeller.DataAccess.DataGenerators
{
    public class FakeDataGenerator
    {
        public static Faker<Customer> GetCustomerGenerator()
        {
            return new Faker<Customer>()
                .RuleFor(c => c.Name, f => f.Name.FullName(f.PickRandom<Name.Gender>()))
                .RuleFor(c => c.Address, f => f.Address.FullAddress())
                .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Email, (f, c) => f.Internet.ExampleEmail(c.Name));
        }
        
        public static Faker<Product> GetProductGenerator()
        {
            return new Faker<Product>()
                .RuleFor(c => c.Name, f => f.Commerce.ProductName())
                .RuleFor(c => c.Price, f => f.Random.Decimal(0.0M, 1000.0M))
                .RuleFor(c => c.StockBalance, f => f.Random.Number(0, 10_000))
                .RuleFor(c => c.ProductTypeId, f => f.Random.Number(1, 10))
                .RuleFor(c => c.ProductDiscountId, f => f.Random.Number(1, 11))
                .RuleFor(c => c.ProductMeasureId, f => f.Random.Number(1, 6))
                .RuleFor(c => c.Info, f => f.Commerce.ProductDescription());
        }
        
        public static Faker<Provider> GetProviderGenerator()
        {
            return new Faker<Provider>()
                .RuleFor(c => c.Name, f => f.Company.CompanyName())
                .RuleFor(c => c.Info, f => f.Internet.UrlWithPath())
                .RuleFor(c => c.Address, f => f.Address.FullAddress())
                .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Email, f => f.Internet.ExampleEmail());
        }
    }
}