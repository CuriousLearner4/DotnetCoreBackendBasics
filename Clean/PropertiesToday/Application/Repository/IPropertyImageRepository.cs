
using Domain;

namespace Application.Repository
{
    public interface IPropertyImageRepository
    {
        Task<Image> GetImageByIdAsync(int id);
        Task<List<Image>> GetAllAsync();
        Task AddAsync(Image image);
        Task UpdateAsync(Image image);
        Task DeleteAsync(Image image);
        
    }
}
