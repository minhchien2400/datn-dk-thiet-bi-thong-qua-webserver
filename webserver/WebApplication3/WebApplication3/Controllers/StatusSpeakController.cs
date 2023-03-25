using Microsoft.AspNetCore.Mvc;
using WebApplication3.Service.StatusSpeak;

namespace WebApplication3.Controllers
{
    [Route("v1/api/statusspeak")]
    [ApiController]
    public class StatusSpeakController : ControllerBase
    {
        private readonly IStatusSpeakService _statusSpeakService;
        public StatusSpeakController(IStatusSpeakService statusSpeakService)
        {
            _statusSpeakService = statusSpeakService;
        }

        // GET api/ lấy giá trị/trạng thái của loa trong database và hiển thị lên UI-remote

        [HttpGet("/getstatusspeak/{id}")]
        public async Task<IActionResult> GetStatusSpeak(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _statusSpeakService.getStatusSpeak(id));
        }
    }
}
