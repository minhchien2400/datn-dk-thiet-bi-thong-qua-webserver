using Microsoft.AspNetCore.Mvc;
using WebApplication3.Service.StatusAir;

namespace WebApplication3.Controllers
{
    [Route("v1/api/statusair")]
    [ApiController]
    public class StatusAirController : ControllerBase
    {
        private readonly IStatusAirService _statusAirService;
        public StatusAirController(IStatusAirService statusAirService)
        {
            _statusAirService = statusAirService;
        }

        // GET api/ lấy giá trị nhiệt độ trong database và hiển thị lên UI-remote

        [HttpGet("/getstatusair/{id}")]
        public async Task<IActionResult> GetStatusSpeak(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _statusAirService.getStatusAir(id));
        }

        [HttpPut("/settempair/{id}")]
        public async Task<IActionResult> SetTempAir(int id,int temp)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _statusAirService.setTemp(id,temp));
        }

        [HttpGet("/gettempair/{id}")]
        public async Task<IActionResult> GetTempAir(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _statusAirService.getTempAir(id));
        }
    }
}
