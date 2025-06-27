using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HinhAnhController : ControllerBase
    {
        private readonly IHinhAnhService _service;

        public HinhAnhController(IHinhAnhService service)
        {
            _service = service;
        }

        [HttpPost("UploadHinhAnh")]
        public async Task<IActionResult> UploadHinhAnh([FromForm] IFormFile file, [FromForm] string metadata)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            // Deserialize metadata
            var hinhAnhMetadata = System.Text.Json.JsonSerializer.Deserialize<HinhAnh>(metadata);
            if (hinhAnhMetadata == null)
            {
                return BadRequest("Invalid metadata.");
            }

            var hinhAnh = new HinhAnh
            {
                LoaiFileHinhAnh = file.ContentType,
                DataHinhAnh = await ReadFileAsync(file),
                IdSanPhamChiTiet = hinhAnhMetadata.IdSanPhamChiTiet,
                TrangThai = hinhAnhMetadata.TrangThai
            };

            var result = await _service.UploadAsync(hinhAnh);
            if (!result)
            {
                return StatusCode(500, "Error while saving the image.");
            }

            return Ok("Image uploaded successfully");
        }

        private async Task<byte[]> ReadFileAsync(IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            return stream.ToArray();
        }

        [HttpGet("GetHinhAnhById")]
        public async Task<IActionResult> GetHinhAnhById(Guid id)
        {
            var hinhAnh = await _service.GetHinhAnhByIdAsync(id);
            if (hinhAnh == null)
            {
                return NotFound("Image not found.");
            }
            return File(hinhAnh.DataHinhAnh, hinhAnh.LoaiFileHinhAnh);
        }

        [HttpDelete("DeleteHinhAnh")]
        public async Task<IActionResult> DeleteHinhAnh(Guid id)
        {
            var hinhAnh = await _service.GetHinhAnhByIdAsync(id);
            if (hinhAnh == null)
            {
                return NotFound("Image not found.");
            }

            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                return StatusCode(500, "Error while deleting the image.");
            }

            return Ok("Image deleted successfully.");
        }

        [HttpGet("GetAllHinhAnh")]
        public async Task<IActionResult> GetAllHinhAnh()
        {
            var hinhAnhs = await _service.GetHinhAnhsAsync();
            return Ok(hinhAnhs);
        }

        [HttpGet("GetHinhAnhsBySanPhamChiTietId")]
        public async Task<IActionResult> GetHinhAnhsBySanPhamChiTietId(Guid sanPhamChiTietId)
        {
            var hinhAnhs = await _service.GetHinhAnhsBySanPhamChiTietId(sanPhamChiTietId);
            if (hinhAnhs == null || !hinhAnhs.Any())
            {
                return NotFound("No images found for the specified product detail ID.");
            }

            return Ok(hinhAnhs);
        }
    }
}
