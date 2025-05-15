using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Repo;
using LibraryManagementSystem.Repositories.UnitOfWork;
using LibraryManagementSystem.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services
{
    public class DiscardingService : IDiscardingService
    {
        private readonly IGenericRepository<LibraryBook> _libraryBookRepo;
        private readonly IGenericRepository<Discarding> _discardingRepo;
        private readonly IUnitOfWork _unitOfWork;

        public DiscardingService(
            IGenericRepository<LibraryBook> libraryBookRepo,
            IGenericRepository<Discarding> discardingRepo,
            IUnitOfWork unitOfWork)
        {
            _libraryBookRepo = libraryBookRepo;
            _discardingRepo = discardingRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task DiscardBookAsync(int libraryBookID, string discardReason)
        {
            var libraryBook = await _libraryBookRepo.GetByIdAsync(libraryBookID);
            if (libraryBook == null || libraryBook.Total_Copies <= 0)
            {
                throw new Exception("No copies available in the library for discarding.");
            }

            var discarding = new Discarding
            {
                LibraryBookID = libraryBookID,
                Discarding_Reason = discardReason,
                Discarding_Date = DateTime.Now,
            };

            await _discardingRepo.AddAsync(discarding);

            libraryBook.Total_Copies--;
            libraryBook.Available_Copies--;

            _libraryBookRepo.Update(libraryBook);

            await _unitOfWork.SaveAsync();
        }
    }
}
