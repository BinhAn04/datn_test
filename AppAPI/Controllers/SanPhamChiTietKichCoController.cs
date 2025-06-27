using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks; // Thêm namespace này để sử dụng Task

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SanPhamChiTietKichCoController : ControllerBase
    {
        private readonly ISanPhamChiTietKichCoService _service;

        public SanPhamChiTietKichCoController(ISanPhamChiTietKichCoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKichCo()
        {
            var kichCos = await _service.GetSanPhamChiTietKichCoAsync();
            return Ok(kichCos);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var sanPhamChiTietKichCo = await _service.GetKichCoByIdAsync(id);
                if (sanPhamChiTietKichCo == null)
                {
                    return NotFound($"SanPhamChiTietKichCo with ID {id} not found.");
                }
                return Ok(sanPhamChiTietKichCo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/SanPhamChiTietKichCo/them
        [HttpPost("them")]
        public async Task<IActionResult> Post(SanPhamChiTietKichCo sanPhamChiTietKichCo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.CreateAsync(sanPhamChiTietKichCo);
                return Ok(sanPhamChiTietKichCo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/SanPhamChiTietKichCo/Sua
        [HttpPut("Sua")]
        public async Task<IActionResult> Put(SanPhamChiTietKichCo sanPhamChiTietKichCo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.UpdateAsync(sanPhamChiTietKichCo);
                return Ok(sanPhamChiTietKichCo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/SanPhamChiTietKichCo/Xoa?id=<guid>
        [HttpDelete("Xoa")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("kichCoIds")]
        public async Task<IActionResult> GetKichCoIdsBySanPhamChiTietId(Guid sanPhamChiTietId)
        {
            try
            {
                var kichCoIds = await _service.GetKichCoIdsBySanPhamChiTietId(sanPhamChiTietId);
                if (kichCoIds == null || !kichCoIds.Any())
                {
                    return NotFound($"No MauSac IDs found for SanPhamChiTiet with ID {sanPhamChiTietId}.");
                }
                return Ok(kichCoIds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("sanphamchitietids")]
        public async Task<IActionResult> GetSanPhamChiTietIdsByKichCoId(Guid kichCoId)
        {
            try
            {
                var sanPhamChiTietIds = await _service.GetSanPhamChiTietIdsByKichCoIdAsync(kichCoId);
                if (sanPhamChiTietIds == null || !sanPhamChiTietIds.Any())
                {
                    return NotFound($"No SanPhamChiTiet IDs found for KichCo with ID {kichCoId}.");
                }
                return Ok(sanPhamChiTietIds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}