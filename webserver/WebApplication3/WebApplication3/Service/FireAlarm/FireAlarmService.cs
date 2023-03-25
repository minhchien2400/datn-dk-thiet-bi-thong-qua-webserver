using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Service.FireAlarm
{
    public class FireAlarmService : IFireAlarmService
    {
        private readonly SmartHomeDbContext _firealarmDbContext;
        public FireAlarmService(SmartHomeDbContext firealarmDbContext)
        {
            _firealarmDbContext = firealarmDbContext;
        }

        // hàm thực hiện lấy giá trị của cảm biến lửa từ ESP8266 gửi lên và update giá trị mới vào data
        public async Task<int> setFireAlarm(int id, int key)
        {
            FireAlarms fireAlarms = new FireAlarms();
            FireAlarms oldFireAlarm = _firealarmDbContext.Set<FireAlarms>().Where(f => f.Id == id).FirstOrDefault();
            fireAlarms.Id = id;
            fireAlarms.KeyFire = key;
            _firealarmDbContext.Remove(oldFireAlarm);
            _firealarmDbContext.Add(fireAlarms);
            _firealarmDbContext.SaveChanges();
            return key;
        }

        // hàm lấy ra giá trị của cảm biến lửa trong database cho UI
        public async Task<int> getKeyFireAlarm(int id)
        {
            var keyFire = await _firealarmDbContext.Set<FireAlarms>().Where(f => f.Id == id).Select(o => o.KeyFire).FirstOrDefaultAsync();
            return keyFire;
        }
        public async Task<FireAlarms> getFireAlarm(int id)
        {
            var fireAlarms = await _firealarmDbContext.Set<FireAlarms>().Where(f => f.Id == id).FirstOrDefaultAsync();
            return fireAlarms;
        }
    }
}
