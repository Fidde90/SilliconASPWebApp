using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity>(DataContext dataContext) where TEntity : class
    {
        private readonly DataContext _context = dataContext;

        public virtual async Task<TEntity> AddToDB(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllFromDB()
        {
            try
            {
                var list = await _context.Set<TEntity>().ToListAsync();
                if (list != null)
                    return list;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public virtual async Task<TEntity> GetOneFromDB(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var user = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
                if (user != null)
                    return user;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public virtual async Task<TEntity> UpdateEntityInDB(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entityToUpdate = _context.Set<TEntity>().FirstOrDefault(predicate);
                if (entityToUpdate != null)
                {
                    _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                    await _context.SaveChangesAsync();
                    return entityToUpdate;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return null!;
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var exists = await _context.Set<TEntity>().AnyAsync(predicate);
                if (exists)
                    return true;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return false;
        }
    }
}
