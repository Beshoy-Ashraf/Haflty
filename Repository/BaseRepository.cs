using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Haflty.Models.Context;
using Haflty.Repository.InterFace;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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


      public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken, string[]? includes = null)
      {
            IQueryable<T> query = _dbContext.Set<T>();
            if (includes != null)
                  foreach (var item in includes)
                        query.Include(item);

            return await query.Where(criteria).ToListAsync(cancellationToken) ?? throw new KeyNotFoundException("No Data found."); ;
      }
      public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken)
      {

            return await _dbContext.Set<T>().Where(criteria).ToListAsync(cancellationToken) ?? throw new KeyNotFoundException("No Data found."); ;
      }
      public async Task<T> AddAsync(T entities, CancellationToken cancellationToken)
      {
            await _dbContext.Set<T>().AddAsync(entities, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entities;
      }
      public async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities, CancellationToken cancellationToken)
      {
            await _dbContext.AddRangeAsync(entities, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entities;
      }
      public async Task<IActionResult> DeleteEntityAsync(Guid id, CancellationToken cancellationToken)
      {
            var entity = await _dbContext.Set<T>().FindAsync(id, cancellationToken);
            if (entity == null)
                  throw new KeyNotFoundException("Entity not found");
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new OkResult();
      }

}

