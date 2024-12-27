using Application.Repository;
using Domain;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repository
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly ApplicationDbContext _context;
        public PropertyImageRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task AddAsync(Image image)
        {
            await _context.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Image image)
        {
            _context.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Image>> GetAllAsync()
        {
           return await (from image in _context.Images select image).ToListAsync();
        }

        public async Task<Image> GetImageByIdAsync(int id)
        {
            return await (from image in _context.Images where image.Id == id select image).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Image image)
        {
            _context.Update(image);
            await _context.SaveChangesAsync();
        }
    }
}
