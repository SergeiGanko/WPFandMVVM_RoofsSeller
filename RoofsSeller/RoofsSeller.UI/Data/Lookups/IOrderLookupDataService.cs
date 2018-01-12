using RoofsSeller.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoofsSeller.UI.Data
{
    public interface IOrderLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetOrderLookupAsync();
    }
}
