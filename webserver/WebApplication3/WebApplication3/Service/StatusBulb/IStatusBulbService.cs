using WebApplication3.Models;

namespace WebApplication3.Service.StatusBulb
{
    public interface IStatusBulbService
    {
        Task<int> getStatus(int id);
        Task<int> getColor(int id);
        Task<StatusBulbs> getStatusBulb(int id);
    }
}
