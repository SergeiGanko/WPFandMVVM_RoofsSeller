using System.Collections.Generic;
using System.Threading.Tasks;
using RoofsSeller.Model;

namespace RoofsSeller.UI.Data.Lookups
{
    public interface ICustomerLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetCustomerLookupAsync();
    }
}