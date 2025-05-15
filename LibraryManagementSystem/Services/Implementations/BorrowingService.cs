using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Repo;
using LibraryManagementSystem.Repositories.UnitOfWork;
using LibraryManagementSystem.Services.Interfaces;

public class BorrowingService : IBorrowingService
{
    private readonly IGenericRepository<Borrowing> _borrowingRepo;
    private readonly IGenericRepository<LibraryBook> _libraryBookRepo;
    private readonly IGenericRepository<Book> _BookRepo;
    private readonly IGenericRepository<Banned_User> _bannedRepo;
    private readonly IPenaltyService _penaltyService;
    private readonly IUnitOfWork _unitOfWork;

    public BorrowingService(
        IGenericRepository<Borrowing> borrowingRepo,
        IGenericRepository<LibraryBook> libraryBookRepo,
        IGenericRepository<Book> bookRepo,
        IGenericRepository<Banned_User> bannedRepo,
        IPenaltyService penaltyService,
        IUnitOfWork unitOfWork)
    {
        _borrowingRepo = borrowingRepo;
        _libraryBookRepo = libraryBookRepo;
        _BookRepo = bookRepo;
        _bannedRepo = bannedRepo;
        _penaltyService = penaltyService;
        _unitOfWork = unitOfWork;
    }
    public async Task AddBorrowingAsync(Borrowing borrowing)
    {
        bool isBanned = await _bannedRepo.AnyAsync(b => b.User_Id == borrowing.User_Id);
        if (isBanned)
            throw new Exception("User is banned and cannot borrow books.");

        var libraryBook = await _libraryBookRepo.GetByIdAsync(borrowing.LibraryBookID)
            ?? throw new Exception($"LibraryBook with ID {borrowing.LibraryBookID} not found.");

        var book = await _BookRepo.GetByIdAsync(libraryBook.BookID)
            ?? throw new Exception($"Book with ID {libraryBook.BookID} not found.");

        if (book.IsLocked)
            throw new Exception($"Book '{book.Title}' is locked and cannot be borrowed.");

        if (libraryBook.Available_Copies <= 0)
            throw new Exception($"No available copies for LibraryBook ID {libraryBook.LibraryBookID}.");

        libraryBook.Available_Copies--;
        _libraryBookRepo.Update(libraryBook);

        borrowing.Date_Of_Borrowing = DateTime.UtcNow;
        borrowing.Return_Date = borrowing.Date_Of_Borrowing.AddDays(borrowing.NumberOfDays);
        borrowing.IsReturned = false;

        await _borrowingRepo.AddAsync(borrowing);

            await _unitOfWork.SaveAsync();
        



    }


    public async Task ReturnBookAsync(int borrowingId)
    {
        var borrowing = await _borrowingRepo.GetByIdAsync(borrowingId);
        if (borrowing == null)
            throw new Exception("Borrowing not found");

        if (borrowing.IsReturned)
            throw new Exception("The book has already been returned.");

        borrowing.Actual_Return_Date = DateTime.UtcNow;
        borrowing.IsReturned = true;

        if (borrowing.Actual_Return_Date > borrowing.Return_Date)
        {
            int lateDays = (borrowing.Actual_Return_Date - borrowing.Return_Date).Days;
            await _penaltyService.AddPenaltyAsync(borrowing.Borrowing_Id, lateDays);
        }

        var libraryBook = await _libraryBookRepo.GetByIdAsync(borrowing.LibraryBookID);
        if (libraryBook != null)
        {
            libraryBook.Available_Copies++;
            _libraryBookRepo.Update(libraryBook);
        }

        _borrowingRepo.Update(borrowing);
        await _unitOfWork.SaveAsync();
    }

}
