using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KichCoController : ControllerBase
    {
        private readonly IKichCoService _service;

        public KichCoController(IKichCoService service)
        {
            _service = service;
        }

        // GET: api/KichCo/getall
        [HttpGet("getall")]
        public IActionResult GetAll(string? name)
        {
            try
            {
                var kichCo = _service.GetKichCo(name);
                return Ok(kichCo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/KichCo/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var kichCo = _service.GetKichCoById(id);
                if (kichCo == null)
                {
                    return NotFound($"KichCo with ID {id} not found.");
                }
                return Ok(kichCo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/KichCo/them
        [HttpPost("them")]
        public IActionResult Post(KichCo kichCo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Create(kichCo);
                return Ok(kichCo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/KichCo/Sua
        [HttpPut("Sua")]
        public IActionResult Put(KichCo kichCo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Update(kichCo);
                return Ok(kichCo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/KichCo/Xoa?id=<guid>
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
        [HttpGet("getkichcobyid")]
        public async Task<ActionResult<List<KichCo>>> GetKichCoBySanPhamId(Guid sanPhamId)
        {
            var kichCos = await _service.GetKichCoBySanPhamId(sanPhamId);
            return Ok(kichCos);
        }

        [HttpGet("getbyids")]
        public async Task<IActionResult> GetByIds([FromQuery] List<Guid> kichCoIds)
        {
            if (kichCoIds == null || !kichCoIds.Any())
            {
                return BadRequest("Please provide at least one size ID.");
            }

            try
            {
                var kichCos = await _service.GetKichCoByIdsAsync(kichCoIds);
                return Ok(kichCos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}