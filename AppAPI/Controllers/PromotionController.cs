using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _service;

        public PromotionController(IPromotionService service)
        {
            _service = service;
        }

        // GET: api/Promotion
        [HttpGet("GetAllPromotions")]
        public async Task<IActionResult> GetAllPromotions()
        {
            var promotions = await _service.GetPromotionsAsync();
            return Ok(promotions);
        }

        // POST: api/Promotion
        [HttpPost("CreatePromotion")]
        public async Task<ActionResult<Promotion>> CreatePromotion(Promotion promotion)
        {
            var result = await _service.CreateAsync(promotion);
            if (!result)
            {
                return StatusCode(500, "Error while saving the Promotion");
            }
            return Ok(result);
            
        }

        // PUT: api/Promotion/5
        [HttpPut("UpdatePromotion/{id}")]
        public async Task<ActionResult<Promotion>> UpdatePromotion(Guid id, Promotion promotion)
        {
            if (id != promotion.IdPromotion)
            {
                return BadRequest("Id mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.UpdateAsync(promotion);
                if (result)
                {
                    return Ok(promotion);
                }
                return NotFound("Promotion not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdatePromotion: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE: api/Promotion/5
        [HttpDelete("DeletePromotion")]
        public async Task<IActionResult> DeletePromotion(Guid id)
        {
            var promotion = await _service.GetPromotionByIdAsync(id);
            if(promotion == null)
            {
                return NotFound("Promotion not found");
            }
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                return StatusCode(500, "Error while deleting the Promotion");
            }
            return Ok("Promotion deleted successfully.");
        }
        [HttpGet("GetPromotionById")]
        public async Task<IActionResult> GetPromotionById(Guid id)
        {
            var promotion = await _service.GetPromotionByIdAsync(id);
            if (promotion == null)
            {
                return NotFound("Promotion not found.");
            }
            return Ok(promotion);
        }
    }
}