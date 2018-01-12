using RoofsSeller.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoofsSeller.UI.Data.Lookups
{
    public interface IOrderStateLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetOrderStateLookupAsync();
    }
}
