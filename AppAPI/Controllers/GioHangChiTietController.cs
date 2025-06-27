using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GioHangChiTietController : ControllerBase
	{
		private readonly IGioHangChiTietService _service;

		public GioHangChiTietController(IGioHangChiTietService service)
		{
			_service = service;
		}

		// GET: api/GioHangChiTiet/getall
		[HttpGet("getall")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var gioHangChiTietList = await _service.GetAllAsync();
				return Ok(gioHangChiTietList);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// GET: api/GioHangChiTiet/getbyid?id=<guid>
		[HttpGet("getbyid")]
		public async Task<IActionResult> Get(Guid id)
		{
			try
			{
				var gioHangChiTiet = await _service.GetByIdAsync(id);
				if (gioHangChiTiet == null)
				{
					return NotFound($"GioHangChiTiet with ID {id} not found.");
				}
				return Ok(gioHangChiTiet);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// POST: api/GioHangChiTiet/them
		[HttpPost("them")]
		public async Task<IActionResult> Post([FromBody] GioHangChiTiet gioHangChiTiet)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				await _service.AddAsync(gioHangChiTiet);
				return CreatedAtAction(nameof(Get), new { id = gioHangChiTiet.IdGioHangChiTiet }, gioHangChiTiet);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// PUT: api/GioHangChiTiet/sua
		[HttpPut("sua")]
		public async Task<IActionResult> Put([FromBody] GioHangChiTiet gioHangChiTiet)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				await _service.UpdateAsync(gioHangChiTiet);
				return Ok(gioHangChiTiet);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// DELETE: api/GioHangChiTiet/xoa?id=<guid>
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

        [HttpGet("getbygiohangid")]
        public async Task<IActionResult> GetByGioHangId(Guid gioHangId)
        {
            try
            {
                var gioHangChiTietList = await _service.GetByGioHangIdAsync(gioHangId);
                if (gioHangChiTietList == null || gioHangChiTietList.Count == 0)
                {
                    return NotFound($"No GioHangChiTiet found for GioHangId {gioHangId}.");
                }
                return Ok(gioHangChiTietList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("removeitems")]
        public async Task<IActionResult> RemoveItemsFromCart([FromBody] RemoveItemsRequest request)
        {
            if (request.ProductDetailIds == null || !request.ProductDetailIds.Any())
            {
                return BadRequest("No product detail IDs provided.");
            }

            try
            {
                await _service.RemoveItemsFromCartAsync(request.CartId, request.ProductDetailIds);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Đối tượng yêu cầu
        public class RemoveItemsRequest
        {
            public Guid CartId { get; set; }
            public List<Guid> ProductDetailIds { get; set; }
        }
        [HttpGet("gettotalquantity")]
        public async Task<IActionResult> GetTotalQuantityBySanPhamChiTietId(Guid sanPhamChiTietId, Guid cartId)
        {
            try
            {
                var totalQuantity = await _service.GetTotalQuantityBySanPhamChiTietIdAsync(sanPhamChiTietId, cartId);
                return Ok(totalQuantity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getbyproductidandcartid")]
        public async Task<IActionResult> GetByProductIdAndCartId(Guid sanPhamChiTietId, Guid cartId)
        {
            try
            {
                var gioHangChiTiet = await _service.GetByProductIdAndCartIdAsync(sanPhamChiTietId, cartId);
                if (gioHangChiTiet == null)
                {
                    return NotFound($"No GioHangChiTiet found for ProductId {sanPhamChiTietId} and CartId {cartId}.");
                }
                return Ok(gioHangChiTiet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getbyids")]
        public async Task<IActionResult> GetByIds([FromQuery] List<Guid> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                {
                    return BadRequest("No IDs provided.");
                }

                var gioHangChiTietList = await _service.GetByIdsAsync(ids);
                if (gioHangChiTietList == null || gioHangChiTietList.Count == 0)
                {
                    return NotFound("No GioHangChiTiet found for the provided IDs.");
                }

                return Ok(gioHangChiTietList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
