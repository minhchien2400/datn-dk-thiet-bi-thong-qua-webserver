using WebApplication3.Models;

namespace WebApplication3.Service.StatusSpeak
{
    public interface IStatusSpeakService
    {
        Task<StatusSpeaks> getStatusSpeak(int id);
    }
}
