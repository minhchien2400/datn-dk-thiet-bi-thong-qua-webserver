using WebApplication3.Models;

namespace WebApplication3.Service.TempHum
{
    public interface ITempHumService
    {
        Task<string> setTempHum(int id,int temp,int hum);
        Task<int> getTemp(int id);
        Task<int> getHum(int id);
    }
}
