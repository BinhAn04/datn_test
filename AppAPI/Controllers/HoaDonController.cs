using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonService _service;

        public HoaDonController(IHoaDonService service)
        {
            _service = service;
        }

        // GET: api/HoaDon/getall
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var hoaDonList = await _service.GetAllAsync();
                return Ok(hoaDonList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/HoaDon/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var hoaDon = await _service.GetByIdAsync(id);
                if (hoaDon == null)
                {
                    return NotFound($"HoaDon with ID {id} not found.");
                }
                return Ok(hoaDon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/HoaDon/them
        [HttpPost("them")]
        public async Task<IActionResult> Post([FromBody] HoaDon hoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.AddAsync(hoaDon);
                return CreatedAtAction(nameof(Get), new { id = hoaDon.IdHoaDon }, hoaDon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/HoaDon/sua
        [HttpPut("sua")]
        public async Task<IActionResult> Put([FromBody] HoaDon hoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.UpdateAsync(hoaDon);
                return Ok(hoaDon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/HoaDon/xoa?id=<guid>
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

        [HttpGet("getbyordernumber")]
        public async Task<IActionResult> GetByOrderNumber(string orderNumber)
        {
            try
            {
                var hoaDon = await _service.GetByOrderNumberAsync(orderNumber);
                if (hoaDon == null)
                {
                    return NotFound($"HoaDon with order number {orderNumber} not found.");
                }
                return Ok(hoaDon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getbycustomerid")]
        public async Task<IActionResult> GetHoaDonsByCustomerId(Guid customerId)
        {
            try
            {
                var hoaDons = await _service.GetHoaDonsByCustomerIdAsync(customerId);
                if (hoaDons == null || !hoaDons.Any())
                {
                    return NotFound($"No HoaDon found for Customer ID {customerId}.");
                }
                return Ok(hoaDons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getbymadan")]
        public async Task<IActionResult> GetByMaDon(string maDon)
        {
            try
            {
                var hoaDon = await _service.GetByMaDonAsync(maDon);
                if (hoaDon == null)
                {
                    return NotFound($"Hóa đơn với mã đơn {maDon} không được tìm thấy.");
                }
                return Ok(hoaDon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ nội bộ: {ex.Message}");
            }
        }
    }
}
