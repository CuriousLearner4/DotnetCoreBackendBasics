using Microsoft.AspNetCore.Mvc;
using StudentAPI.Handlers.Interface;

namespace StudentAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly IHandler _handlerChain;
        public StudentAPIController(IHandlerService handlerService)
        {
            _handlerChain = handlerService.CreateHandlerChain();
        }
        [HttpGet("{roll:int}")]
        public async Task<ActionResult<string>> GetInfo(int roll)
        {
         
            var data = await _handlerChain.Handle(roll);
            if (data == null) return BadRequest();
            return Ok(data.Value.ToShortDateString());
            #region BeforeChainofResponsibilities
            //var cacheData = _memoryCache.Get<DateTime?>(Convert.ToString(roll));
            //if (cacheData == null)
            //{
            //    var dateOfBirth = await _repository.GetDOBAsync(roll);
            //    if (dateOfBirth == null)
            //    {
            //        return BadRequest();
            //    }
            //    var cacheEntryOptions = new MemoryCacheEntryOptions
            //    {
            //        AbsoluteExpiration = DateTime.Now.AddMinutes(30)
            //    };
            //    _memoryCache.Set(Convert.ToString(roll), dateOfBirth, cacheEntryOptions);
            //    cacheData = dateOfBirth;
            //}
            //return Ok(cacheData);
            #endregion
        }

    }
}
