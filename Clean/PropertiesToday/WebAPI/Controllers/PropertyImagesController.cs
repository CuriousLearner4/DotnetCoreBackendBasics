using Application.Feature.PropertyImages.Commands;
using Application.Feature.PropertyImages.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/PropertyImages")]
    [ApiController]
    public class PropertyImagesController : ControllerBase
    {
        private readonly ISender _mediatrSender;
        public PropertyImagesController(ISender meadiatrSender) {
            _mediatrSender = meadiatrSender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewPropertyImage([FromBody] NewImage newImage)
        {
            bool isSuccesful = await _mediatrSender.Send(new CreatePropertyImageRequest(newImage));
            if (isSuccesful)
            {
                return Ok("Property Image Added Successfully");
            }
            return BadRequest("Failed to add property image");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePropertyImage([FromBody] UpdateImage updateImage)
        {
            bool isSuccessful = await _mediatrSender.Send(new UpdatePropertyImageRequest(updateImage));
            if (isSuccessful)
            {
                return Ok("Property Image Updated Successfully");
            }
            return NotFound("Property Image not found");
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ViewImage>> GetPropertyByIdImage(int id)
        {
            ViewImage viewImage = await _mediatrSender.Send(new GetPropertyImageByIdRequest(id));
            if (viewImage != null)
            {
                return Ok(viewImage);
            }
            return NotFound("No property image found");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewImage>>> GetAllPropertyImages()
        {
            IEnumerable<ViewImage> properties = await _mediatrSender.Send(new GetAllPropertyImagesRequest());
            return Ok(properties);
            
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePropertyImage(int id)
        {
            bool isSuccessful = await _mediatrSender.Send(new DeletePropertyImageRequest(id));
            if (!isSuccessful)
            {
                return NotFound("Property image Not found");
            }
            return NoContent();
        }
    }
}
