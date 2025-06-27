using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhMucController : ControllerBase
    {
        private readonly IDanhMucService _service;

        public DanhMucController(IDanhMucService service)
        {
            _service = service;
        }

        // GET: api/DanhMuc/getall
        [HttpGet("getall")]
        public IActionResult GetAll(string? name)
        {
            try
            {
                var danhMuc = _service.GetDanhMuc(name);
                return Ok(danhMuc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/DanhMuc/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var danhMuc = _service.GetDanhMucById(id);
                if (danhMuc == null)
                {
                    return NotFound($"DanhMuc with ID {id} not found.");
                }
                return Ok(danhMuc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/DanhMuc/them
        [HttpPost("them")]
        public IActionResult Post(DanhMuc danhMuc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Create(danhMuc);
                return Ok(danhMuc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/DanhMuc/Sua
        [HttpPut("Sua")]
        public IActionResult Put(DanhMuc danhMuc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Update(danhMuc);
                return Ok(danhMuc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/DanhMuc/Xoa?id=<guid>
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
    }
}