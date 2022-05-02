using Domain;
using Domain.Intefaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();

        }
        public async Task AddAsync(T entity)
        {
             _dbSet.Add(entity);
             await SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
