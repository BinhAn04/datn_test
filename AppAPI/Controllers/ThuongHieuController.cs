using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuongHieuController : ControllerBase
    {
        private readonly IThuongHieuService _service;

        public ThuongHieuController(IThuongHieuService service)
        {
            _service = service;
        }

        // GET: api/ThuongHieu/getall
        [HttpGet("getall")]
        public IActionResult GetAll(string? name)
        {
            try
            {
                var thuongHieu = _service.GetThuongHieu(name);
                return Ok(thuongHieu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ThuongHieu/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var thuongHieu = _service.GetThuongHieuById(id);
                if (thuongHieu == null)
                {
                    return NotFound($"ThuongHieu with ID {id} not found.");
                }
                return Ok(thuongHieu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/ThuongHieu/them
        [HttpPost("them")]
        public IActionResult Post(ThuongHieu thuongHieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Create(thuongHieu);
                return Ok(thuongHieu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/ThuongHieu/Sua
        [HttpPut("Sua")]
        public IActionResult Put(ThuongHieu thuongHieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Update(thuongHieu);
                return Ok(thuongHieu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/ThuongHieu/Xoa?id=<guid>
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