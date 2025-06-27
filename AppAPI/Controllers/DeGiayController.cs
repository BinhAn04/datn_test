using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeGiayController : ControllerBase
    {
        private readonly IDeGiayService _service;

        public DeGiayController(IDeGiayService service)
        {
            _service = service;
        }

        // GET: api/DeGiay/getall
        [HttpGet("getall")]
        public IActionResult GetAll(string? name)
        {
            try
            {
                var deGiay = _service.GetDeGiay(name);
                return Ok(deGiay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/DeGiay/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var deGiay = _service.GetDeGiayById(id);
                if (deGiay == null)
                {
                    return NotFound($"DeGiay with ID {id} not found.");
                }
                return Ok(deGiay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/DeGiay/them
        [HttpPost("them")]
        public IActionResult Post(DeGiay deGiay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Create(deGiay);
                return Ok(deGiay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/DeGiay/Sua
        [HttpPut("Sua")]
        public IActionResult Put(DeGiay deGiay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Update(deGiay);
                return Ok(deGiay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/DeGiay/Xoa?id=<guid>
        [HttpDelete("Xoa")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}