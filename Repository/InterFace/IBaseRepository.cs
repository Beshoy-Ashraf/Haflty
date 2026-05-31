namespace Haflty.Repository.InterFace;

public interface IBaseRepository<T> where T : class
{

      Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
      Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
}
