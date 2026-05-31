using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace Haflty.Repository.InterFace;

public interface IBaseRepository<T> where T : class
{

      Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
      Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
      Task<IEnumerable<T>> Find(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken, string[]? includes = null);
      Task<IEnumerable<T>> Find(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken);
      Task<T> AddAsync(T entities, CancellationToken cancellationToken);
      Task<IEnumerable<T>> AddRange(IEnumerable<T> entities, CancellationToken cancellationToken);
      Task<IActionResult> DeleteEntityAsync(Guid id, CancellationToken cancellationToken);
}
