namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IBorrowingService
    {
        Task AddBorrowingAsync(Borrowing borrowing);

        Task ReturnBookAsync(int borrowingId);

    }
    }
