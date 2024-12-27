
using Domain;

namespace Application.Repository
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(int id);
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(Property property);

    }
}
