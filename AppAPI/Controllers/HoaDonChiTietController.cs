using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonChiTietController : ControllerBase
    {
        private readonly IHoaDonChiTietService _service;

        public HoaDonChiTietController(IHoaDonChiTietService service)
        {
            _service = service;
        }

        // GET: api/HoaDonChiTiet/getall
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var hoaDonChiTietList = await _service.GetAllAsync();
                return Ok(hoaDonChiTietList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/HoaDonChiTiet/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var hoaDonChiTiet = await _service.GetByIdAsync(id);
                if (hoaDonChiTiet == null)
                {
                    return NotFound($"HoaDonChiTiet with ID {id} not found.");
                }
                return Ok(hoaDonChiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/HoaDonChiTiet/them
        [HttpPost("them")]
        public async Task<IActionResult> Post([FromBody] List<HoaDonChiTiet> hoaDonChiTietList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.AddAsync(hoaDonChiTietList); 
                return CreatedAtAction(nameof(Get), null, hoaDonChiTietList); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/HoaDonChiTiet/sua
        [HttpPut("sua")]
        public async Task<IActionResult> Put([FromBody] List<HoaDonChiTiet> hoaDonChiTietList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.UpdateAsync(hoaDonChiTietList);
                return Ok(hoaDonChiTietList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/HoaDonChiTiet/xoa?id=<guid>
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

        [HttpGet("getbyidhd")]
        public async Task<IActionResult> GetByIdHoaDon(Guid idhoadon)
        {
            try
            {
                var hoaDonChiTietList = await _service.GetByIdHoaDonAsync(idhoadon);
                if (hoaDonChiTietList == null || !hoaDonChiTietList.Any())
                {
                    return NotFound($"No HoaDonChiTiet found for HoaDon ID {idhoadon}.");
                }
                return Ok(hoaDonChiTietList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("gettotalquantity")]
        public async Task<IActionResult> GetTotalQuantityBySanPhamChiTietId(Guid sanPhamChiTietId, Guid hDCTId)
        {
            try
            {
                var totalQuantity = await _service.GetTotalQuantityBySanPhamChiTietIdAsync(sanPhamChiTietId, hDCTId);
                return Ok(totalQuantity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getbyidandproduct")]
        public async Task<IActionResult> GetByIdAndProduct(Guid idhoadon, Guid idsanphamchitiet)
        {
            try
            {
                var hoaDonChiTiet = await _service.GetByIdAndProduct(idhoadon, idsanphamchitiet);
                if (hoaDonChiTiet == null)
                {
                    return NotFound($"HoaDonChiTiet not found for HoaDon ID {idhoadon} and SanPhamChiTiet ID {idsanphamchitiet}.");
                }
                return Ok(hoaDonChiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
