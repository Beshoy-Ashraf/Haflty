using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Haflty.Models.Context;
using Haflty.Repository.InterFace;
using Microsoft.EntityFrameworkCore;

namespace Haflty.Repository;

public class BaseRepository<T>(AppDBContext appDBContext) : IBaseRepository<T> where T : class
{
      private readonly AppDBContext _dbContext = appDBContext;

      public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
      {
            return await _dbContext.Set<T>().FindAsync(id, cancellationToken) ?? throw new KeyNotFoundException("No Data found.");
      }
      public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
      {
            return await _dbContext.Set<T>().ToListAsync(cancellationToken) ?? throw new KeyNotFoundException("No Data found.");
      }


      public async Task<T> Find(Expression<Func<T, bool>> criteria, string[]? includes = null)
      {
            IQueryable<T> query = _dbContext.Set<T>();
            if (includes != null)
                  foreach (var item in includes)
                        query.Include(item);

            return await query.FirstOrDefaultAsync(criteria) ?? throw new KeyNotFoundException("No Data found."); ;
      }
      public async Task<T> AddAsync(T entities)
      {
            await _dbContext.Set<T>().AddAsync(entities);
            return entities;
      }

}

