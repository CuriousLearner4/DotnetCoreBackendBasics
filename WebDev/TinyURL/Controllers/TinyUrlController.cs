using Microsoft.AspNetCore.Mvc;
using TinyURL.Models;
using TinyURL.Models.DTO;
using TinyURL.Services;
using TinyURL.Data;

namespace TinyURL.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TinyUrlController: ControllerBase
    {
        [HttpPost("shorten")]
        public async Task<ActionResult<ShortURLData>> Shorten([FromBody]ShortenDTO shortenDTO)
        {
            if (shortenDTO == null) {
                return BadRequest();
            }
            ShortURLData shortURLData = new ShortURLData();
            shortURLData.LongURL = shortenDTO.LongURL;
            shortURLData.ShortURL = ShortURLGenerator.generate();
            shortURLData.AccessDateTime = new List<string>();
            //if (shortenDTO.CustomAlias == null)
            //{
            //    shortURLData.ShortURL = ShortURLGenerator.generate();
            //}
            //else
            //{
            //    shortURLData.ShortURL = shortenDTO.CustomAlias;
            //}
            DataStore.db[shortURLData.ShortURL] = shortURLData;
            return CreatedAtRoute("GetAlias", new {alias =  shortURLData.ShortURL},shortURLData);
        }

        [HttpGet("{alias}", Name = "GetAlias")]
        public IActionResult GetAlias(string alias)
        {
            if (alias == null) {
                return BadRequest();
            }
            if (DataStore.db.ContainsKey(alias) == false)
            {
                return NotFound();
            };
            var shortURLData = DataStore.db[alias];
            shortURLData.AccessCount++;
            shortURLData.AccessDateTime.Add(DateTime.Now.ToString());
            return Redirect(shortURLData.LongURL);
        }

        [HttpGet("analytics/{alias}")]
        public ActionResult<ShortURLData> GetAnalytics(string alias)
        {
            if (alias == null)
            {
                return BadRequest();
            }
            if (!DataStore.db.ContainsKey(alias))
            return NotFound();
            var shortURLData = DataStore.db[alias];
            return Ok(shortURLData);
        }

        [HttpPut("update/{alias}")]
        public IActionResult UpdateAlias(string alias, [FromBody]UpdateDTO updateDTO)
        {
            if (updateDTO == null) return BadRequest();
            if (DataStore.db.ContainsKey(alias) == false) return NotFound();
            var shortUrlData = DataStore.db[alias];
            DataStore.db.Remove(alias);
            shortUrlData.ShortURL = updateDTO.CustomAlias;
            DataStore.db[shortUrlData.ShortURL] = shortUrlData;
            return Ok();
        }

        [HttpDelete("delete/{alias}")]
        public IActionResult DeleteAlias(string alias)
        {
            if (DataStore.db.ContainsKey(alias) == false) return NotFound();
            DataStore.db.Remove(alias);
            return Ok();
        }
    }
}
