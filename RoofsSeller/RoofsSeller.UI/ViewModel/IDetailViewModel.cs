using System.Threading.Tasks;

namespace RoofsSeller.UI.ViewModel
{
    public interface IDetailViewModel
    {
        Task LoadAsync(int Id);
        bool HasChanges { get; }
        int Id { get; }
    }
}