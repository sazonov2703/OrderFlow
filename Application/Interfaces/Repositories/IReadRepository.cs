using System.Linq.Expressions;
using Microsoft.AspNetCore.OData.Query;

namespace Application.Interfaces.Repositories;

public interface IReadRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IQueryable<T>> GetQueryableAsync(ODataQueryOptions<T> oDataQueryOptions, CancellationToken cancellationToken);
    
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
}