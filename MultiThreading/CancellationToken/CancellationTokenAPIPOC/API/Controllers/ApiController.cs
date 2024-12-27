using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        
        private readonly ApiServices _service;
        private readonly RequestManager _requestManager;
        private readonly ILogger<ApiController> _logger;

        public ApiController(ApiServices service, RequestManager requestManger, ILogger<ApiController> logger)
        {
            _service = service;
            _requestManager = requestManger;
            _logger = logger;
        }
        [HttpGet("A")]
        public async Task<IActionResult> EndpointA(string userId)
        {
            string s;
            var cancellationToken = _requestManager.RegisterOperation(userId);
            try
            {
                await _service.ProcessEndpointAAsync(cancellationToken);
                return Ok("Endpoint A processed sucessfully.");
            }
            catch(OperationCanceledException ex)
            {
                _logger.LogInformation("cancelled previous request and saved resources");
                return StatusCode(409, "operation was cancelled due to a new request");
            }
            finally
            {
                _requestManager.RemoveOperation(userId);
            }
        }
        [HttpGet("B")]
        public async Task<IActionResult> EndpointB(string userId)
        {
            var cancellationToken = _requestManager.RegisterOperation(userId);
            try
            {
                await _service.ProcessEndpointBAsync(cancellationToken);
                return Ok("Endpoint B processed sucessfully.");
            }
            catch(OperationCanceledException ex)
            {
                _logger.LogInformation("cancelled previous request and saved resources");
                return StatusCode(409, "operation was cancelled due to a new request");
            }
            finally
            {
                _requestManager.RemoveOperation(userId);
            }
        }
    }
}
