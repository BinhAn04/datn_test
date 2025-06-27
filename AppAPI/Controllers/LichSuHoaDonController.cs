using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuHoaDonController : ControllerBase
    {
        private readonly ILichSuHoaDonService _service;

        public LichSuHoaDonController(ILichSuHoaDonService service)
        {
            _service = service;
        }

        // GET: api/LichSuHoaDon/getall
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lichSuHoaDonList = await _service.GetAllAsync();
                return Ok(lichSuHoaDonList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/LichSuHoaDon/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var lichSuHoaDon = await _service.GetByIdAsync(id);
                if (lichSuHoaDon == null)
                {
                    return NotFound($"LichSuHoaDon with ID {id} not found.");
                }
                return Ok(lichSuHoaDon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/LichSuHoaDon/them
        [HttpPost("them")]
        public async Task<IActionResult> Post([FromBody] LichSuHoaDon lichSuHoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.AddAsync(lichSuHoaDon);
                return CreatedAtAction(nameof(Get), new { id = lichSuHoaDon.IdLichSuHoaDon }, lichSuHoaDon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/LichSuHoaDon/sua
        [HttpPut("sua")]
        public async Task<IActionResult> Put([FromBody] LichSuHoaDon lichSuHoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.UpdateAsync(lichSuHoaDon);
                return Ok(lichSuHoaDon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/LichSuHoaDon/xoa?id=<guid>
        [HttpDelete("xoa")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getbyidhoadon")]
        public async Task<IActionResult> GetByIdHoaDon(Guid idHoaDon)
        {
            try
            {
                var lichSuHoaDonList = await _service.GetByIdHoaDonAsync(idHoaDon);
                if (lichSuHoaDonList == null || !lichSuHoaDonList.Any())
                {
                    return NotFound($"LichSuHoaDon with IdHoaDon {idHoaDon} not found.");
                }
                return Ok(lichSuHoaDonList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
