using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        public BaseRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public virtual async Task<TEntity> AddToDB(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {

            }

            return null!;
        }

        public async virtual Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var Exists = await _context.Set<TEntity>().AnyAsync(predicate);
                if (Exists)
                    return true;
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return false;
        }
    }
}
