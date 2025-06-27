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
    public class LichSuSuDungVoucherController : ControllerBase
    {
        private readonly ILichSuSuDungVoucherService _service;

        public LichSuSuDungVoucherController(ILichSuSuDungVoucherService service)
        {
            _service = service;
        }

        // GET: api/LichSuSuDungVoucher/getall
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lichSuSuDungVoucherList = await _service.GetAllAsync();
                return Ok(lichSuSuDungVoucherList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/LichSuSuDungVoucher/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var lichSuSuDungVoucher = await _service.GetByIdAsync(id);
                if (lichSuSuDungVoucher == null)
                {
                    return NotFound($"LichSuSuDungVoucher with ID {id} not found.");
                }
                return Ok(lichSuSuDungVoucher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/LichSuSuDungVoucher/them
        [HttpPost("them")]
        public async Task<IActionResult> Post([FromBody] LichSuSuDungVoucher lichSuSuDungVoucher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.AddAsync(lichSuSuDungVoucher);
                return CreatedAtAction(nameof(Get), new { id = lichSuSuDungVoucher.IdLichSuVoucher }, lichSuSuDungVoucher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/LichSuSuDungVoucher/sua
        [HttpPut("sua")]
        public async Task<IActionResult> Put([FromBody] LichSuSuDungVoucher lichSuSuDungVoucher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.UpdateAsync(lichSuSuDungVoucher);
                return Ok(lichSuSuDungVoucher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/LichSuSuDungVoucher/xoa?id=<guid>
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
    }
}