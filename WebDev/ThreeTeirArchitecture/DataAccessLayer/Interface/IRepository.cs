using DataAccessLayer.Models;
namespace DataAccessLayer.Interface
{
    public interface IRepository<T>  where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        //Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        //Task DeleteAsync(T entity);
        Task SaveAsync();

    }
}
