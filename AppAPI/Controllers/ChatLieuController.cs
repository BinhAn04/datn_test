using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatLieuController : ControllerBase
    {
        private readonly IChatLieuService _service;

        public ChatLieuController(IChatLieuService service)
        {
            _service = service;
        }

        // GET: api/ChatLieu/getall
        [HttpGet("getall")]
        public IActionResult GetAll(string? name)
        {
            try
            {
                var chatLieu = _service.GetChatLieu(name);
                return Ok(chatLieu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ChatLieu/getbyid?id=<guid>
        [HttpGet("getbyid")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var chatLieu = _service.GetChatLieuById(id);
                if (chatLieu == null)
                {
                    return NotFound($"ChatLieu with ID {id} not found.");
                }
                return Ok(chatLieu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/ChatLieu/them
        [HttpPost("them")]
        public IActionResult Post(ChatLieu chatLieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Create(chatLieu);
                return Ok(chatLieu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/ChatLieu/Sua
        [HttpPut("Sua")]
        public IActionResult Put(ChatLieu chatLieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.Update(chatLieu);
                return Ok(chatLieu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/ChatLieu/Xoa?id=<guid>
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