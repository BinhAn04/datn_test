using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamService _service;

        public SanPhamController(ISanPhamService service)
        {
            _service = service;
        }

        // GET: api/SanPham/getall
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(string? name)
        {
            try
            {
                var sanPham = await _service.GetSanPhamAsync(name); // Await the async method
                return Ok(sanPham);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getallspclient")]
        public async Task<IActionResult> GetAllSP(string? name)
        {
            try
            {
                var sanPham = await _service.GetSanPhamClientAsync(name);
                return Ok(sanPham);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getSanPhamDanhMuc")]
        public async Task<IActionResult> GetSanPhamByCategory(Guid idDanhMuc)
        {
            try
            {
                var sanPham = await _service.GetSanPhamByCategory(idDanhMuc);
                return Ok(sanPham);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // GET: api/SanPham/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                // Kiểm tra ID có hợp lệ không
                if (id == Guid.Empty)
                {
                    return BadRequest("ID cannot be empty.");
                }

                Console.WriteLine($"Fetching product with ID: {id}");
                var sanPham = await _service.GetSanPhamByIdAsync(id); // Gọi phương thức async

                if (sanPham == null)
                {
                    return NotFound($"SanPham with ID {id} not found.");
                }

                return Ok(sanPham);
            }
            catch (Exception ex)
            {
                // Ghi lại ngoại lệ (cân nhắc sử dụng một framework ghi nhật ký)
                Console.WriteLine($"An error occurred while retrieving the product: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // POST: api/SanPham/them
        [HttpPost("them")]
        public async Task<IActionResult> Post([FromBody] SanPham sanPham) // Use [FromBody] to bind the request body
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.CreateAsync(sanPham); // Await the async method
                if (!result)
                {
                    return BadRequest("Failed to create the product.");
                }
                return CreatedAtAction(nameof(Get), new { id = sanPham.IdSanPham }, sanPham); // Assuming SanPham has an Id property
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/SanPham/Sua
        [HttpPut("Sua")]
        public async Task<IActionResult> Put([FromBody] SanPham sanPham) // Use [FromBody] to bind the request body
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.UpdateAsync(sanPham); // Await the async method
                if (!result)
                {
                    return BadRequest("Failed to update the product.");
                }
                return Ok(sanPham);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/SanPham/Xoa?id=<guid>
        [HttpDelete("Xoa")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _service.DeleteAsync(id); // Await the async method
                if (!result)
                {
                    return NotFound($"SanPham with ID {id} not found.");
                }
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getthuonghieu")]
        public async Task<IActionResult> GetThuongHieuBySanPhamId(Guid sanPhamId)
        {
            try
            {
                var thuongHieu = await _service.GetThuongHieuBySanPhamIdAsync(sanPhamId);
                if (thuongHieu == null)
                {
                    return NotFound($"ThuongHieu for SanPham with ID {sanPhamId} not found.");
                }
                return Ok(thuongHieu);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving the brand for product ID {sanPhamId}: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
       
    }
}