using RoofsSeller.DataAccess;
using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Data.Repositories
{
    public class OrderStateRepository :
        GenericRepository<OrderState, RoofsSellerDbContext>, IOrderStateRepository
    {
        public OrderStateRepository(RoofsSellerDbContext context) : base(context)
        {
        }
    }
}
