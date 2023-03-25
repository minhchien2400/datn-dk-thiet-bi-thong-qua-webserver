using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Service.StatusSpeak
{
    public class StatusSpeakService : IStatusSpeakService
    {

        private readonly SmartHomeDbContext _statusSpeakDbContext;
        public StatusSpeakService(SmartHomeDbContext statusSpeakDbContext)
        {
            _statusSpeakDbContext = statusSpeakDbContext;
        }

        // hàm lấy trạng thái của loa trong db 
        public async Task<StatusSpeaks> getStatusSpeak(int id)
        {
            var statusSpeak = _statusSpeakDbContext.Set<StatusSpeaks>().Where(s => s.Id == id).FirstOrDefault();
            return statusSpeak;
        }
    }
}

