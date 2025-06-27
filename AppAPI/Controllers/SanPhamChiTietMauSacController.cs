using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SanPhamChiTietMauSacController : ControllerBase
    {
        private readonly ISanPhamChiTietMauSacService _service;

        public SanPhamChiTietMauSacController(ISanPhamChiTietMauSacService service)
        {
            _service = service;
        }

        // GET: api/SanPhamChiTietMauSac
        [HttpGet]
        public IActionResult GetAllMauSac()
        {
            var mauSacs = _service.GetSanPhamChiTietMauSac();
            return Ok(mauSacs);
        }

        // GET: api/SanPhamChiTietMauSac/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public IActionResult Get(Guid id)
        {
            try
            {
                // Gọi đến service để lấy danh sách IdSanPhamChiTiet dựa trên id màu sắc
                var sanPhamChiTietMauSacIds = _service.GetSanPhamChiTietIdsByMauSacId(id);

                // Kiểm tra xem danh sách có rỗng không
                if (sanPhamChiTietMauSacIds == null || !sanPhamChiTietMauSacIds.Any())
                {
                    return NotFound($"No SanPhamChiTiet found for MauSac ID {id}.");
                }

                // Trả về danh sách IdSanPhamChiTiet
                return Ok(sanPhamChiTietMauSacIds);
            }
            catch (Exception ex)
            {
                // Trả về mã lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/SanPhamChiTietMauSac/them
        [HttpPost("them")]
        public IActionResult Post(SanPhamChiTietMauSac sanPhamChiTietMauSac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Create(sanPhamChiTietMauSac);
                return Ok(sanPhamChiTietMauSac);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/SanPhamChiTietMauSac/Sua
        [HttpPut("Sua")]
        public IActionResult Put(SanPhamChiTietMauSac sanPhamChiTietMauSac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Update(sanPhamChiTietMauSac);
                return Ok(sanPhamChiTietMauSac);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/SanPhamChiTietMauSac/Xoa?id=<guid>
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
        [HttpGet("mauSacIds")]
        public async Task<IActionResult> GetMauSacIdsBySanPhamChiTietId(Guid id )
        {
            try
            {
                var mauSacIds = await _service.GetMauSacIdsBySanPhamChiTietId(id);
                if (mauSacIds == null || !mauSacIds.Any())
                {
                    return NotFound($"No MauSac IDs found for SanPhamChiTiet with ID {id}.");
                }
                return Ok(mauSacIds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}