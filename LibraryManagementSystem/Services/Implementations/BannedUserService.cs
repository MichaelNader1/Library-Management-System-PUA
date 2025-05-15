using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Repo;
using LibraryManagementSystem.Repositories.UnitOfWork;
using LibraryManagementSystem.Services.Interfaces;

namespace LibraryManagementSystem.Services.Implementations
{
    public class BannedUserService : IBannedUserService
    {
        private readonly IGenericRepository<Banned_User> _bannedRepo;
        private readonly IGenericRepository<Borrowing> _borrowingRepo;
        private readonly IGenericRepository<LibraryBook> _libraryBookRepo;
        private readonly IGenericRepository<Book> _bookRepo;
        private readonly Lazy<IPenaltyService> _penaltyService;
        private readonly IUnitOfWork _unitOfWork;

        public BannedUserService(
            IGenericRepository<Banned_User> bannedRepo,
            IGenericRepository<Borrowing> borrowingRepo,
            IGenericRepository<LibraryBook> libraryBookRepo,
            IGenericRepository<Book> bookRepo,
            Lazy<IPenaltyService> penaltyService,
            IUnitOfWork unitOfWork)
        {
            _bannedRepo = bannedRepo;
            _borrowingRepo = borrowingRepo;
            _libraryBookRepo = libraryBookRepo;
            _bookRepo = bookRepo;
            _penaltyService = penaltyService;
            _unitOfWork = unitOfWork;
        }

        public async Task BanUserAsync(string userId)
        {
            var existingBan = await _bannedRepo.FindAsync(b => b.User_Id == userId);

            if (existingBan != null) return; 

            var bannedUser = new Banned_User
            {
                User_Id = userId,
                Ban_Start_Date = DateTime.Now,
                Ban_End_Date = DateTime.Now.AddDays(120) 
            };

            await _bannedRepo.AddAsync(bannedUser);
            await _unitOfWork.SaveAsync(); 
        }

        public async Task<bool> IsUserBannedAsync(string userId)
        {
            var bannedUser = await _bannedRepo.FindAsync(b => b.User_Id == userId && b.Ban_End_Date > DateTime.Now);
            return bannedUser != null; 
        }

        public async Task RemoveExpiredBansAsync()
        {
            var expiredBans = await _bannedRepo.FindAllAsync(b => b.Ban_End_Date < DateTime.Now);
            _bannedRepo.DeleteRange(expiredBans); 
            await _unitOfWork.SaveAsync(); 
        }
    }
}
