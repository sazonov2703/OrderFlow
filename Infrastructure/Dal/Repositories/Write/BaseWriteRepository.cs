using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Repositories;

namespace Infrastructure.Dal.Repositories.Write;

public abstract class BaseWriteRepository<T>(OrderFlowDbContext context) 
    : IWriteRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();
    
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        await Task.Run(() => _dbSet.Update(entity), cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(id, cancellationToken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity {typeof(T).Name} with Id {id} does not exist.");
        }
        
        await Task.Run(() => _dbSet.Remove(entity), cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}