using System.Threading.Tasks;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IDiscardingService
    {
        Task DiscardBookAsync(int libraryBookID, string discardReason);
    }
}
