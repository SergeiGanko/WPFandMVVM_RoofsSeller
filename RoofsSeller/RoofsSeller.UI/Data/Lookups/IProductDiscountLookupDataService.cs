using RoofsSeller.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoofsSeller.UI.Data.Lookups
{
    public interface IProductDiscountLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetProductDiscountLookupAsync();
    }
}
