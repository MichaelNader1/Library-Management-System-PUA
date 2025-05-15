using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Repo;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryManagementSystem.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> SaveAsync();

    }

}
