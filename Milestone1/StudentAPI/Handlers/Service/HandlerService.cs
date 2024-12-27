using StudentAPI.Handlers.Interface;

namespace StudentAPI.Handlers.Service
{
    public class HandlerService : IHandlerService
    {
        
        private readonly CacheHandler _cacheHandler;
        private readonly DbHandler _dbHandler;
        private readonly ApiHandler _apiHandler;
        
        public HandlerService( CacheHandler cacheHandler, DbHandler dbHandler, ApiHandler apiHandler)
        {
            _cacheHandler = cacheHandler;
            _dbHandler = dbHandler;
            _apiHandler = apiHandler;
        }

        public Handler CreateHandlerChain()
        {
            _cacheHandler.setNextHandler(_dbHandler).setNextHandler(_apiHandler);
            return _cacheHandler;
        }
        #region UsingIServiceProvider
        //private readonly IServiceProvider _serviceProvider;
        //public HandlerService(IServiceProvider serviceProvider)
        //{
        //    _serviceProvider = serviceProvider;
        //}
        //public Handler CreateHandlerChain()
        //{

        //    var cachedHandler = _serviceProvider.GetRequiredService<CacheHandler>();
        //    var dbHandler = _serviceProvider.GetRequiredService<DbHandler>();
        //    var apiHandler = _serviceProvider.GetRequiredService<ApiHandler>();
        //}
        #endregion
    }
}
