using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Repo;
using LibraryManagementSystem.Repositories.UnitOfWork;
using LibraryManagementSystem.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services
{
    public class TransferService : ITransferService
    {
        private readonly IGenericRepository<LibraryBook> _libraryBookRepo;
        private readonly IGenericRepository<Transferring> _transferringRepo;
        private readonly IUnitOfWork _unitOfWork;

        public TransferService(
            IGenericRepository<LibraryBook> libraryBookRepo,
            IGenericRepository<Transferring> transferringRepo,
            IUnitOfWork unitOfWork)
        {
            _libraryBookRepo = libraryBookRepo;
            _transferringRepo = transferringRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task TransferBookAsync(int sourceLibraryBookID, int destinationLibraryBookID)
        {
            var sourceLibraryBook = await _libraryBookRepo.GetByIdAsync(sourceLibraryBookID);

            if (sourceLibraryBook == null)
                throw new Exception($"LibraryBook with ID {sourceLibraryBookID} not found.");

            if (sourceLibraryBook.Total_Copies <= 0)
                throw new Exception($"No total copies to transfer. Total_Copies = {sourceLibraryBook.Total_Copies}");

            if (sourceLibraryBook.Available_Copies <= 0)
                throw new Exception($"No available copies to transfer. Available_Copies = {sourceLibraryBook.Available_Copies}");

            var destinationLibraryBook = await _libraryBookRepo.GetByIdAsync(destinationLibraryBookID);

            if (destinationLibraryBook == null)
                throw new Exception($"LibraryBook with ID {destinationLibraryBookID} not found.");

            destinationLibraryBook.Total_Copies++;
            destinationLibraryBook.Available_Copies++;
            _libraryBookRepo.Update(destinationLibraryBook);

            sourceLibraryBook.Total_Copies--;
            sourceLibraryBook.Available_Copies--;
            _libraryBookRepo.Update(sourceLibraryBook);

            var transferring = new Transferring
            {
                SourceLibraryBookID = sourceLibraryBookID,
                DestinationLibraryBookID = destinationLibraryBookID,
                Transferring_Date = DateTime.Now,
            };
            await _transferringRepo.AddAsync(transferring);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving changes: " + ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
