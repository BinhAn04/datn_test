using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionSanPhamChiTietController : ControllerBase
    {
        private readonly IPromotionSanPhamChiTietService _service;

        public PromotionSanPhamChiTietController(IPromotionSanPhamChiTietService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPromotions()
        {
            var promotions = await _service.GetPromotionSanPhamChiTietAsync();
            return Ok(promotions);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var promotion = await _service.GetPromotionByIdAsync(id);
                if (promotion == null)
                {
                    return NotFound($"Promotion with ID {id} not found.");
                }
                return Ok(promotion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/PromotionSanPhamChiTiet/them
        [HttpPost("them")]
        public async Task<IActionResult> Post(PromotionSanPhamChiTiet promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.CreateAsync(promotion);
                return Ok(promotion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/PromotionSanPhamChiTiet/Sua
        [HttpPut("Sua")]
        public async Task<IActionResult> Put(PromotionSanPhamChiTiet promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.UpdateAsync(promotion);
                return Ok(promotion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/PromotionSanPhamChiTiet/Xoa?id=<guid>
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

        [HttpGet("promotionsBySanPhamChiTietId")]
        public async Task<IActionResult> GetPromotionsBySanPhamChiTietId(Guid sanPhamChiTietId)
        {
            try
            {
                var promotion = await _service.GetPromotionsBySanPhamChiTietIdAsync(sanPhamChiTietId);
                if (promotion == null)
                {
                    return NotFound($"No promotions found for SanPhamChiTiet with ID {sanPhamChiTietId}.");
                }
                return Ok(promotion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("promotionsById")]
        public async Task<IActionResult> GetPromotionSanPhamChiTietsByPromotionId(Guid promotionId)
        {
            try
            {
                var promotions = await _service.GetPromotionSanPhamChiTietsByPromotionIdAsync(promotionId);
                if (promotions == null || !promotions.Any())
                {
                    return NotFound($"No SanPhamChiTiet found for Promotion with ID {promotionId}.");
                }
                return Ok(promotions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}