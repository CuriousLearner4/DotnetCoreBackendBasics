using System;
using System.Collections.Generic;
using Application.Repository;
using Domain;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext _context;

        public PropertyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Property property)
        {
            _context.Remove(property);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Property>> GetAllAsync()
        {
            return await (from property in _context.Properties select property).ToListAsync();
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            return await (from property in _context.Properties where property.Id == id select property).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }
    }
}
