using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Service.StatusBulb
{
    public class StatusBulbService : IStatusBulbService
    {
        private readonly SmartHomeDbContext _statusBulbDbContext;
        public StatusBulbService(SmartHomeDbContext statusBulbDbContext)
        {
            _statusBulbDbContext = statusBulbDbContext;
        }

        // hàm lấy giá trị trạng thái của đèn (1 or 0)
        public async Task<int> getStatus(int id)
        {
            var status =_statusBulbDbContext.Set<StatusBulbs>().Where(s => s.Id == id).Select(o => o.Status).FirstOrDefault();
            return status;
        }

        // hàm lấy màu sắc của đèn 
        public async Task<int> getColor(int id)
        {
            var color =_statusBulbDbContext.Set<StatusBulbs>().Where(s => s.Id == id).Select(o => o.Color).FirstOrDefault();
            return color;
        }

        // lấy các thông số / trạng thái của đèn
        public async Task<StatusBulbs> getStatusBulb(int id)
        {
            var statusBulbs =_statusBulbDbContext.Set<StatusBulbs>().Where(s => s.Id == id).FirstOrDefault();
            return statusBulbs;
        }

    }
}
