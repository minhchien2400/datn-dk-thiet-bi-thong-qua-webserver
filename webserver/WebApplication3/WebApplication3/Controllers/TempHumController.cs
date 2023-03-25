using Microsoft.AspNetCore.Mvc;
using WebApplication3.Service.TempHum;

namespace WebApplication3.Controllers
{
    [Route("v1/api/temphum")]
    [ApiController]
    public class TempHumController : ControllerBase
    {
        private readonly ITempHumService _tempHumService;
        public TempHumController(ITempHumService tempHumService)
        {
            _tempHumService = tempHumService;
        }

        // GET api/ lấy giá trị nhiệt độ trong database và hiển thị lên UI-remote

        [HttpGet("/gettemp/{id}")]
        public async Task<IActionResult> GetTemp(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _tempHumService.getTemp(id));
        }

        

        // gửi giá trị nhiệt độ , độ ẩm lên server (from ESP8266)
        [HttpGet("/settemphum/{id}")]
        public async Task<IActionResult> Put(int id, int temp,int hum)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _tempHumService.setTempHum(id, temp, hum));
        }

        // GET api/ lấy giá trị độ ẩm trong database và hiển thị lên UI-remote
        [HttpGet("/gethum/{id}")]
        public async Task<IActionResult> GetHum(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _tempHumService.getHum(id));
        }
    }
}
