using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MauSacController : ControllerBase
    {
        private readonly IMauSacService _service;

        public MauSacController(IMauSacService service)
        {
            _service = service;
        }

        // GET: api/MauSac/getall
        [HttpGet("getall")]
        public IActionResult GetAll(string? name)
        {
            try
            {
                var mauSac = _service.GetMauSac(name);
                return Ok(mauSac);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/MauSac/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var mauSac = _service.GetMauSacById(id);
                if (mauSac == null)
                {
                    return NotFound($"MauSac with ID {id} not found.");
                }
                return Ok(mauSac);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/MauSac/them
        [HttpPost("them")]
        public IActionResult Post(MauSac mauSac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Create(mauSac);
                return Ok(mauSac);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/MauSac/Sua
        [HttpPut("Sua")]
        public IActionResult Put(MauSac mauSac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Update(mauSac);
                return Ok(mauSac);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/MauSac/Xoa?id=<guid>
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
        [HttpGet("getmausacbyid")]
        public async Task<ActionResult<List<MauSac>>> GetMauSacBySanPhamId(Guid sanPhamId)
        {
            var mauSacs = await _service.GetMauSacBySanPhamId(sanPhamId);
            return Ok(mauSacs);
        }
        [HttpGet("getbyids")]
        public async Task<IActionResult> GetByIds([FromQuery] List<Guid> mauSacIds)
        {
            if (mauSacIds == null || !mauSacIds.Any())
            {
                return BadRequest("Please provide at least one color ID.");
            }

            try
            {
                var mauSacs = await _service.GetMauSacByIdsAsync(mauSacIds);
                return Ok(mauSacs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}