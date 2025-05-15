using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Repo;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _dbcontext;
        private readonly Dictionary<string, object> _repository = new();

        public UnitOfWork(LibraryDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;
            if (!_repository.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_dbcontext);
                _repository.Add(type, repository);
            }
            return _repository[type] as IGenericRepository<TEntity>;
        }

        public async Task<int> SaveAsync() => await _dbcontext.SaveChangesAsync();
        public void Dispose() => _dbcontext.Dispose();

    }

}