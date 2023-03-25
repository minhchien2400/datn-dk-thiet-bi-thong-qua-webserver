using WebApplication3.Models;

namespace WebApplication3.Service.FireAlarm
{
    public interface IFireAlarmService
    {
        Task<int> setFireAlarm(int id, int key);
        Task<int> getKeyFireAlarm(int id);
        Task<FireAlarms> getFireAlarm(int id);
    }
}
