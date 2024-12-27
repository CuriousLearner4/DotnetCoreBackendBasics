using Microsoft.Extensions.Caching.Memory;
using StudentAPI.Data.Repository;
using StudentAPI.Handlers.Interface;

namespace StudentAPI.Handlers
{
    public class DbHandler : Handler,IDbHandler
    {
        private readonly IStudentRepository _repo;
        private readonly IMemoryCache _memoryCache;

        public DbHandler(IStudentRepository repo, IMemoryCache memoryCaching)
        {
            _repo = repo;
            _memoryCache = memoryCaching;
        }

        public async override Task<DateTime?> Handle(int roll)
        {
            var dateOfBirth = await _repo.GetDOBAsync(roll);
            if (dateOfBirth != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30)
                };
                _memoryCache.Set(Convert.ToString(roll), dateOfBirth, cacheEntryOptions);
                return await Task.FromResult(dateOfBirth);
            }
            return await HandleNext(roll);
        }
    }
}
