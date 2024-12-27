using DataAccessLayer.Interface;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataAccessLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbcontext;
        internal DbSet<T> dbset;

        public Repository(ApplicationDbContext dbcontext) { 
            this.dbcontext = dbcontext;
            this.dbset = dbcontext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await dbset.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbset.Remove(entity);
            await SaveAsync();
        }

        //public Task DeleteAsync(int id)
        //{
        //   dbset.Remove()
        //}

        public async Task<IEnumerable<T>> GetAsync()
        {
            IQueryable<T> query = dbset;
            return await query.ToListAsync();
        }

        //public async Task<T> GetByIdAsync(int id)
        //{
        //    return await dbset.FirstOrDefaultAsync()
        //}

        public async Task SaveAsync()
        {
            await dbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbset.Update(entity);
            await SaveAsync();
        }
    }
}
