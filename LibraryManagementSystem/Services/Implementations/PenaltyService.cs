using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Repo;
using LibraryManagementSystem.Repositories.UnitOfWork;
using LibraryManagementSystem.Services.Interfaces;

namespace LibraryManagementSystem.Services.Implementations
{
    public class PenaltyService : IPenaltyService
    {
        private readonly IGenericRepository<Penalty> _penaltyRepository;
        private readonly IGenericRepository<Borrowing> _borrowingRepository;
        private readonly IBannedUserService _bannedUserService;
        private readonly IUnitOfWork _unitOfWork;

        public PenaltyService(
            IGenericRepository<Penalty> penaltyRepository,
            IGenericRepository<Borrowing> borrowingRepository,
            IBannedUserService bannedUserService,
            IUnitOfWork unitOfWork
            )
        {
            _penaltyRepository = penaltyRepository;
            _borrowingRepository = borrowingRepository;
            _bannedUserService = bannedUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task AddPenaltyAsync(int borrowingId, int lateDays)
        {
            var borrowing = await _borrowingRepository.GetByIdAsync(borrowingId);
            if (borrowing == null)
                throw new Exception("Borrowing not found.");

            var penalty = new Penalty
            {
                Borrowing_Id = borrowingId,
                Penalty_Date = DateTime.Now,
                Amount = lateDays * 100
            };

            await _penaltyRepository.AddAsync(penalty);
            await _unitOfWork.SaveAsync();

            var userPenalties = await _penaltyRepository.FindAllAsync(p => p.Borrowing != null && p.Borrowing.User_Id == borrowing.User_Id);
            if (userPenalties.Count() >= 2)
            {
                await _bannedUserService.BanUserAsync(borrowing.User_Id);
            }
        }

        public async Task<int> GetPenaltyCountAsync(string userId)
        {
            var penalties = await _penaltyRepository.FindAllAsync(p => p.Borrowing != null && p.Borrowing.User_Id == userId);
            return penalties.Count();
        }
    }

}
