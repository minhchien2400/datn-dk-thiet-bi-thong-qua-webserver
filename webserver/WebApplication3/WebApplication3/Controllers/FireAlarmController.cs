using Microsoft.AspNetCore.Mvc;
using WebApplication3.Service.FireAlarm;

namespace WebApplication3.Controllers
{
        [Route("v1/api/firealarm")]
        [ApiController]
        public class FireAlarmController : ControllerBase
        {
            private readonly IFireAlarmService _fireAlarmService;
            public FireAlarmController(IFireAlarmService fireAlarmService)
            {
                _fireAlarmService = fireAlarmService;
            }

        // Lấy ra giá trị của cảm biến lửa trong database
            [HttpGet("/getkeyfire/{id}")]
            public async Task<IActionResult> GetKeyFire(int id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                return Ok(await _fireAlarmService.getKeyFireAlarm(id));
            }



            // Gửi giá trị của cảm biến lửa lên server (from ESP8266)
            [HttpGet("/setkeyfire/{id}")]
            public async Task<IActionResult> Put(int id, int key)
            {
                if (id == null)
                {
                    return NotFound();
                }
                return Ok(await _fireAlarmService.setFireAlarm(id,key));
            }


            [HttpGet("/getfirealarm/{id}")]
            public async Task<IActionResult> GetFire(int id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                return Ok(await _fireAlarmService.getFireAlarm(id));
            }
        }
    }
