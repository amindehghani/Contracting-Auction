using ContractingAuction.Core.Entities;

namespace ContractingAuction.Core.Interfaces.IRepositories;

public interface IBaseRepository<T> where T : Base
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T model);
    Task UpdateAsync(T model);
    Task DeleteAsync(T model);
}