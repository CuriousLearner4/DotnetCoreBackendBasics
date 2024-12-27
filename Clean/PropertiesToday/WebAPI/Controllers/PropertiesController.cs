using Application.Feature.Properties.Commands;
using Application.Feature.Properties.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/Properties")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ISender _mediatrSender;
        public PropertiesController(ISender meadiatrSender) {
            _mediatrSender = meadiatrSender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewProperty([FromBody] NewProperty newPropertyRequest)
        {
            bool isSuccesful = await _mediatrSender.Send(new CreatePropertyRequest(newPropertyRequest));
            if (isSuccesful)
            {
                return Ok("Property Created Successfully");
            }
            return BadRequest("Failed to create property");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProperty([FromBody] UpdateProperty updateProperty)
        {
            bool isSuccessful = await _mediatrSender.Send(new UpdatePropertyRequest(updateProperty));
            if (isSuccessful)
            {
                return Ok("Property Updated Successfully");
            }
            return NotFound("Property not found");
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ViewProperty>> GetPropertyById(int id)
        {
            ViewProperty propertyById = await _mediatrSender.Send(new GetPropertyByIdRequest(id));
            if (propertyById != null)
            {
                return Ok(propertyById);
            }
            return NotFound("No property found");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewProperty>>> GetAllProperties()
        {
            IEnumerable<ViewProperty> properties = await _mediatrSender.Send(new GetAllProprtiesRequest());
            return Ok(properties);
            
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            bool isSuccessful = await _mediatrSender.Send(new DeletePropertyRequest(id));
            if (!isSuccessful)
            {
                return NotFound("Property Not found");
            }
            return NoContent();
        }
    }
}
