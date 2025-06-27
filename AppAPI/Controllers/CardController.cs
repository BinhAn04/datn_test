using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _service;

        public CardController(ICardService service)
        {
            _service = service;
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> Get(Guid id) // Thay đổi thành async Task<IActionResult>
        {
            try
            {
                var cartId = await _service.GetCartIdByCustomerIdAsync(id); // Thêm await

                if (cartId == Guid.Empty) // Kiểm tra Guid.Empty thay vì null
                {
                    return NotFound($"Cart with Customer ID {id} not found.");
                }

                return Ok(cartId); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("them")]
        public IActionResult Post(GioHang gioHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Create(gioHang);
                return Ok(gioHang);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
