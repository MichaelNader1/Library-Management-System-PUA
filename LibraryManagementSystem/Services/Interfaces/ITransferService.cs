using System.Threading.Tasks;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface ITransferService
    {
        Task TransferBookAsync(int sourceLibraryBookID, int destinationLibraryID);
    }
}
