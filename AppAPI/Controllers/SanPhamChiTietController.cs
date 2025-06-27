using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamChiTietController : ControllerBase
    {
        private readonly ISanPhamChiTietService _service;

        public SanPhamChiTietController(ISanPhamChiTietService service)
        {
            _service = service;
        }

        // GET: api/SanPhamChiTiet/getall
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var sanPhamChiTiet = _service.GetSanPhamChiTiet();
                return Ok(sanPhamChiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/SanPhamChiTiet/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var sanPhamChiTiet = _service.GetSanPhamChitietById(id);
                if (sanPhamChiTiet == null)
                {
                    return NotFound($"SanPhamChiTiet with ID {id} not found.");
                }
                return Ok(sanPhamChiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/SanPhamChiTiet/them
        [HttpPost("them")]
        public IActionResult Post(SanPhamChiTiet sanPhamChiTiet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Create(sanPhamChiTiet);
                return Ok(sanPhamChiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex.Message}");
            }
        }

        // PUT: api/SanPhamChiTiet/Sua
        [HttpPut("Sua")]
        public async Task<IActionResult> Put(SanPhamChiTiet sanPhamChiTiet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.Update(sanPhamChiTiet); // Gọi phương thức Update bất đồng bộ
                if (result)
                {
                    return Ok(sanPhamChiTiet);
                }
                return NotFound(); // Trả về 404 nếu không tìm thấy sản phẩm chi tiết
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/SanPhamChiTiet/Xoa?id=<guid>
        [HttpDelete("Xoa")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _service.Delete(id); 
                if (result)
                {
                    return Ok(); 
                }
                return NotFound(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/SanPhamChiTiet/getbysanphamid?sanPhamId=<guid>
        [HttpGet("getbysanphamid")]
        public async Task<IActionResult> GetSanPhamChiTietBySanPhamId(Guid sanPhamId)
        {
            try
            {
                var sanPhamChiTietList = await _service.GetSanPhamChiTietBySanPhamId(sanPhamId);
                if (sanPhamChiTietList == null || !sanPhamChiTietList.Any())
                {
                    return NotFound($"No SanPhamChiTiet found for SanPhamId {sanPhamId}.");
                }
                return Ok(sanPhamChiTietList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-by-filter")]
        public async Task<IActionResult> GetIdSanPhamChiTietByFilter([FromQuery] Guid idSanPham, [FromQuery] Guid idKichCo, [FromQuery] Guid idMauSac)
        {
            try
            {
                var result = await _service.GetIdSanPhamChiTietByFilter(idSanPham, idKichCo, idMauSac);
                if (result == null)
                {
                    return NotFound("Không tìm thấy sản phẩm phù hợp");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi server: " + ex.Message);
            }
        }

        [HttpGet("getsanphambysanphamchitiet")]
        public async Task<IActionResult> GetSanPhamByIdSanPhamChiTiet(Guid idSanPhamChiTiet)
        {
            try
            {
                var sanPham = await _service.GetSanPhamByIdSanPhamChiTietAsync(idSanPhamChiTiet);
                if (sanPham == null)
                {
                    return NotFound($"SanPham not found for SanPhamChiTietId {idSanPhamChiTiet}.");
                }
                return Ok(sanPham);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getbyhoadonchitiet")]
        public async Task<IActionResult> GetByIdHoaDonChiTietAsync(Guid id)
        {
            try
            {
                var sanPhamChiTiet = await _service.GetByIdHoaDonChiTietAsync(id);
                if (sanPhamChiTiet == null)
                {
                    return NotFound($"SanPhamChiTiet not found for HoaDonChiTiet ID {id}.");
                }
                return Ok(sanPhamChiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getbyproductcode")]
        public async Task<IActionResult> GetByProductCodeAsync([FromQuery] string productCode)
        {
            try
            {
                var sanPhamChiTiet = await _service.GetByProductCodeAsync(productCode);
                if (sanPhamChiTiet == null)
                {
                    return NotFound($"No SanPhamChiTiet found for product code {productCode}.");
                }
                return Ok(sanPhamChiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

