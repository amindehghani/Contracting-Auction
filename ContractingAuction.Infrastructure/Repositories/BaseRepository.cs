using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Interfaces.IRepositories;
using ContractingAuction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContractingAuction.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T: Base
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> CreateAsync(T model)
    {
        await _dbSet.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task UpdateAsync(T model)
    {
        _dbSet.Update(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T model)
    {
        _dbSet.Remove(model);
        await _dbContext.SaveChangesAsync();
    }
}