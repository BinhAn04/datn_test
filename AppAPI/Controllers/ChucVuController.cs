using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChucVuController : ControllerBase
    {
        private readonly IChucVuService _service;
        public ChucVuController(IChucVuService service)
        {
            _service = service;
        }
        [HttpGet("GetAllChucVu")]
        public async Task<ActionResult<List<ChucVu>>> GetAllCV() 
        { 
            return Ok(await _service.GetAllChucVu());
        }
        [HttpGet("GetIdChucVu")]
        public async Task<IActionResult> GetIdCV(Guid id)
        {
            var chucVu = await _service.GetIdChucVu(id);
           if (chucVu == null)
            {
                return NotFound();
            }
           return Ok(chucVu);
        }
        [HttpGet("GetChucVuId")]
        public async Task<IActionResult> GetChucVuId(Guid? idChucVu)
        {
            var chucVu = await _service.GetChucVuId(idChucVu);
            if(chucVu == null)
            {
                return NotFound();
            }
            return Ok(chucVu);
        }
        [HttpPost("CreateChucVu")]
        public async Task<IActionResult> CreateCV(ChucVu cv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.CreateCV(cv);
            return Ok();
        }
    }
}
