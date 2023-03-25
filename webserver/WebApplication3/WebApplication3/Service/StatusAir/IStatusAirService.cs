using WebApplication3.Models;

namespace WebApplication3.Service.StatusAir
{
    public interface IStatusAirService
    {
        Task<StatusAirs> getStatusAir(int id);
        Task<int> setTemp(int id,int temp);
        Task<int> getTempAir(int id); 
    }
}
