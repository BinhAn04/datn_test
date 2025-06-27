using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KieuDangController : ControllerBase
    {
        private readonly IKieuDangService _service;

        public KieuDangController(IKieuDangService service)
        {
            _service = service;
        }

        // GET: api/KieuDang/getall
        [HttpGet("getall")]
        public IActionResult GetAll(string? name)
        {
            try
            {
                var kieuDang = _service.GetKieuDang(name);
                return Ok(kieuDang);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/KieuDang/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var kieuDang = _service.GetKieuDangById(id);
                if (kieuDang == null)
                {
                    return NotFound($"KieuDang with ID {id} not found.");
                }
                return Ok(kieuDang);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/KieuDang/them
        [HttpPost("them")]
        public IActionResult Post(KieuDang kieuDang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Create(kieuDang);
                return Ok(kieuDang);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/KieuDang/Sua
        [HttpPut("Sua")]
        public IActionResult Put(KieuDang kieuDang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Update(kieuDang);
                return Ok(kieuDang);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/KieuDang/Xoa?id=<guid>
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