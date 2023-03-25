using Microsoft.AspNetCore.Mvc;
using WebApplication3.Service.Remote;

namespace WebApplication3.Controllers
{
    [Route("v1/api/remote")]
    [ApiController]
    public class RemoteController : ControllerBase
    {
        private readonly IRemoteService _remoteService;
        public RemoteController(IRemoteService remoteService)
        {
            _remoteService = remoteService;
        }

        // lấy ra giá trị của keyremote trong database (from ESP8266)
        [HttpGet("/getkeyremote/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _remoteService.getKeyRomote(id));
        }



        // PUT api/ sửa giá trị của keyremote trong database khi click các nút trên remote
        [HttpPut("/setkeyremote/{id}")]
        public async Task<IActionResult> Put(int id, int key)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _remoteService.setKeyRomote(id, key));
        }


        [HttpGet("/getremote/{id}")]
        public async Task<IActionResult> GetRemote(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _remoteService.GetRemote(id));
        }
    }
}
