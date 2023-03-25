using Microsoft.AspNetCore.Mvc;
using WebApplication3.Service.StatusBulb;
using WebApplication3.Service.TempHum;

namespace WebApplication3.Controllers
{

    [Route("v1/api/statusbulb")]
    [ApiController]
    public class StatusBulbController : ControllerBase
    {
        private readonly IStatusBulbService _statusBulbService;
        public StatusBulbController(IStatusBulbService statusBulbService)
        {
            _statusBulbService = statusBulbService;
        }

        // GET api/ lấy giá trị/trạng thái của đèn (1 or 0) để hiển thị lên UI-remote

        [HttpGet("/getstatus/{id}")]
        public async Task<IActionResult> GetStatus(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _statusBulbService.getStatus(id));
        }



        // GET api/ lấy giá màu sắc của đèn để hiển thị lên UI-remote

        [HttpGet("/getcolor/{id}")]
        public async Task<IActionResult> GetColor(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _statusBulbService.getColor(id));
        }


        // GET api/ lấy giá trị/trạng thái của đèn trong database và hiển thị lên UI-remote
        [HttpGet("/getstatusbulb/{id}")]
        public async Task<IActionResult> GetStatusBulb(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _statusBulbService.getStatusBulb(id));
        }
    }
}
