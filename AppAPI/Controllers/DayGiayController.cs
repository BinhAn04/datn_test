//using AppAPI.IService;
//using AppData.Model;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;

//namespace AppAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DayGiayController : ControllerBase
//    {
//        private readonly IDayGiayService _service;

//        public DayGiayController(IDayGiayService service)
//        {
//            _service = service;
//        }

//        // GET: api/DayGiay/getall
//        [HttpGet("getall")]
//        public  IActionResult GetAll(string? name)
//        {
//            try
//            {
//                var dayGiay =  _service.GetDayGiay(name);
//                return Ok(dayGiay);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // GET: api/DayGiay/getbyid?id=<guid>
//        [HttpGet("getbyid")]
//        public IActionResult Get(Guid id)
//        {
//            try
//            {
//                var dayGiay =  _service.GetDayGiayById(id);
//                if (dayGiay == null)
//                {
//                    return NotFound($"DayGiay with ID {id} not found.");
//                }
//                return Ok(dayGiay);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // POST: api/DayGiay/them
//        [HttpPost("them")]
//        public  IActionResult Post(DayGiay dayGiay)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                _service.Create(dayGiay);
//                return Ok(dayGiay);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // PUT: api/DayGiay/Sua
//        [HttpPut("Sua")]
//        public  IActionResult Put(DayGiay dayGiay)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                 _service.Update(dayGiay);
//                return Ok(dayGiay);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // DELETE: api/DayGiay/Xoa?id=<guid>
//        [HttpDelete("Xoa")]
//        public  IActionResult Delete(Guid id)
//        {
//            try
//            {
//                 _service.Delete(id);
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }
//    }
//}