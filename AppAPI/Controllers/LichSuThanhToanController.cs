using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuThanhToanController : ControllerBase
    {
        private readonly ILichSuThanhToanService _service;

        public LichSuThanhToanController(ILichSuThanhToanService service)
        {
            _service = service;
        }

        // GET: api/LichSuThanhToan/getall
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lichSuThanhToanList = await _service.GetAllAsync();
                return Ok(lichSuThanhToanList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/LichSuThanhToan/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var lichSuThanhToan = await _service.GetByIdAsync(id);
                if (lichSuThanhToan == null)
                {
                    return NotFound($"LichSuThanhToan with ID {id} not found.");
                }
                return Ok(lichSuThanhToan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/LichSuThanhToan/them
        [HttpPost("them")]
        public async Task<IActionResult> Post([FromBody] LichSuThanhToan lichSuThanhToan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.AddAsync(lichSuThanhToan);
                return CreatedAtAction(nameof(Get), new { id = lichSuThanhToan.IdLichSuThanhToan }, lichSuThanhToan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/LichSuThanhToan/sua
        [HttpPut("sua")]
        public async Task<IActionResult> Put([FromBody] LichSuThanhToan lichSuThanhToan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.UpdateAsync(lichSuThanhToan);
                return Ok(lichSuThanhToan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/LichSuThanhToan/xoa?id=<guid>
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

        // GET: api/LichSuThanhToan/getbyidhoadon?idhoadon=<guid>
        [HttpGet("getbyidhoadon")]
        public async Task<IActionResult> GetByIdHoaDon(Guid idHoaDon)
        {
            try
            {
                var lichSuThanhToanList = await _service.GetByIdHoaDonAsync(idHoaDon);
                if (lichSuThanhToanList == null || !lichSuThanhToanList.Any())
                {
                    return NotFound($"LichSuThanhToan with IdHoaDon {idHoaDon} not found.");
                }
                return Ok(lichSuThanhToanList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}