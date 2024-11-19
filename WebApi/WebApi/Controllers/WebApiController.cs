using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Dto;
using WebApi.Data;

namespace WebApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/WebApi")]
    [ApiController]

    public class WebApiController : ControllerBase
    {
        private readonly ILogger<WebApiController> _logger;
        public WebApiController(ILogger<WebApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<WebDto>> GetWebs()
        {
            _logger.LogInformation("Getting all Web Apis");
            return Ok(WebStore.webList);
        }

        [HttpGet("{id}", Name = "GetWeb")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<WebDto> GetWeb(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Get Web Error with Id" + id);
                return BadRequest();
            }

            var web = WebStore.webList.FirstOrDefault(i => i.Id == id);
            if (web == null)
            {
                return NotFound();
            }
            //return Ok(WebStore.webList.FirstOrDefault(i=> i.Id==id));
            return Ok(web);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<WebDto> CreateWeb([FromBody] WebDto webDto)
        {
            if (webDto == null)
            {
                return BadRequest(webDto);
            }
            if (webDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            webDto.Id = WebStore.webList.OrderByDescending(i => i.Id).FirstOrDefault().Id + 1;
            WebStore.webList.Add(webDto);
            //return Ok(webDto);
            return CreatedAtRoute("Getweb", new { id = webDto.Id }, webDto);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteWeb")]

        public IActionResult Deleteweb(int id)
        {
            if (id == 0)
            { return BadRequest(); }

            var villa = WebStore.webList.FirstOrDefault(i => i.Id == id);
            if (villa == null)
            { return NotFound(); }
            WebStore.webList.Remove(villa);
            return NoContent();

        }

        [HttpPut("{id:int}", Name="UpdateWeb")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateWeb (int id, [FromBody]WebDto webDto)
        {
           if(webDto == null || id!= webDto.Id)
            {
                return BadRequest();
            }
            var web = WebStore.webList.FirstOrDefault(u=>u.Id == id);
            web.Name = webDto.Name;
            web.Sqft = webDto.Sqft;
            web.Occupancy = webDto.Occupancy;

            
            return NoContent();
        }


    }
}
