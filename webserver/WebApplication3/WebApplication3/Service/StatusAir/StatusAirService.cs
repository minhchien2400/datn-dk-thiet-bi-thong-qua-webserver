using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Service.StatusAir
{
    public class StatusAirService : IStatusAirService
    {

        private readonly SmartHomeDbContext _statusAirDbContext;
        public StatusAirService(SmartHomeDbContext statusAirDbContext)
        {
            _statusAirDbContext = statusAirDbContext;
        }

        // hàm lấy các giá trị / trạng thái của điều hòa trong db 
        public async Task<StatusAirs> getStatusAir(int id)
        {
            var statusAir = _statusAirDbContext.Set<StatusAirs>().Where(a => a.Id == id).FirstOrDefault();
            return statusAir;
        }

        // hàm set giá trị nhiệt độ update vào db

        public async Task<int> setTemp(int id,int temp)
        {
            StatusAirs oldAir = _statusAirDbContext.Set<StatusAirs>().Where(a => a.Id == id).FirstOrDefault();
            StatusAirs newAir = new StatusAirs();
            newAir.Id = id;
            newAir.Status = oldAir.Status;
            newAir.Mode = oldAir.Mode;
            newAir.Speed = oldAir.Speed;
            newAir.Temp = temp;
            _statusAirDbContext.Remove(oldAir);
            _statusAirDbContext.Add(newAir);
            _statusAirDbContext.SaveChanges();
            return temp;
        }
        public async Task<int> getTempAir(int id)
        {
            var tempAir = _statusAirDbContext.Set<StatusAirs>().Where(a => a.Id == id).Select(a => a.Temp).FirstOrDefault();
            return tempAir;
        }
    }
}
