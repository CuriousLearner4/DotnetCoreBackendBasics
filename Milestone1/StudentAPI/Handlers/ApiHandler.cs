using Microsoft.Extensions.Caching.Memory;
using StudentAPI.Handlers.Interface;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace StudentAPI.Handlers
{
    public class ApiHandler : Handler, IHandler
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ApiHandler(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public class DateofBirth
        {
            public DateTime DateOfBirth { get; set; }
        }

        public override async Task<DateTime?> Handle(int roll)
        {
            var httpClientName = _configuration.GetSection("HttpClientConfiguration:StudentHttpClientName").Value!;
            var httpClient = _httpClientFactory.CreateClient(httpClientName);
            var response = await httpClient.GetAsync($"{roll}");
            if (response.IsSuccessStatusCode)
            {
                var dob = await response.Content.ReadFromJsonAsync<DateofBirth>();
                if (dob != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(30)
                    };
                    _memoryCache.Set(Convert.ToString(roll), dob.DateOfBirth, cacheEntryOptions);
                    return await Task.FromResult(dob.DateOfBirth);
                }
            }
            return await HandleNext(roll);
            #region Approach1
            //using (var httpClient = _httpClientFactory.CreateClient(httpClientName))
            //{
            //    using (var response = await httpClient.GetAsync($"{roll}"))
            //    {
            //        if (response.StatusCode == HttpStatusCode.OK)
            //        {
            //            var content = response.Content.ReadAsStringAsync();
            //            var result = content.Result;
            //            DateOfBirth? dob = JsonSerializer.Deserialize<DateOfBirth>(result);
            //            if (dob != null)
            //            {
            //                var cacheEntryOptions = new MemoryCacheEntryOptions
            //                {
            //                    AbsoluteExpiration = DateTime.Now.AddMinutes(30)
            //                };
            //                _memoryCache.Set(Convert.ToString(roll), dob.dateOfBirth, cacheEntryOptions);
            //                return await Task.FromResult(dob.dateOfBirth);
            //            }
            //        }
            //    }
            //}
            #endregion
            #region AnotherApproach

            //DateOfBirth? dob = await httpClient.GetFromJsonAsync<DateOfBirth>($"{roll}");
            //if (dob != null)
            //{
            //    var cacheEntryOptions = new MemoryCacheEntryOptions
            //    {
            //        AbsoluteExpiration = DateTime.Now.AddMinutes(30)
            //    };
            //    _memoryCache.Set(Convert.ToString(roll), dob.dateOfBirth, cacheEntryOptions);
            //    return await Task.FromResult(dob.dateOfBirth);
            //}
            #endregion
        }
    }
}
