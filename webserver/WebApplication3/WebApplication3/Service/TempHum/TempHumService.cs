using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Service.TempHum
{
    public class TempHumService : ITempHumService
    {
        private readonly SmartHomeDbContext _tempHumDbContext;
        public TempHumService(SmartHomeDbContext tempHumDbContext)
        {
            _tempHumDbContext = tempHumDbContext;
        }

        // hàm lấy giá trị nhiệt độ, độ ẩm từ ESP và update vào database
        public async Task<string> setTempHum(int id,int temp,int hum)
        {
            TempHums tempHums = new TempHums();
            TempHums oldTempHum = _tempHumDbContext.Set<TempHums>().Where(o => o.Id == id).FirstOrDefault();
            tempHums.Id = id;
            tempHums.Temp = temp;
            tempHums.Hum = hum;
            _tempHumDbContext.Remove(oldTempHum);
            _tempHumDbContext.Add(tempHums);
            _tempHumDbContext.SaveChanges();
            return temp.ToString()+"---"+hum.ToString();
        }

        // hàm lấy giá trị nhiệt độ từ database để hiển thị lên UI
        public async Task<int> getTemp(int id)
        {
            var temp =_tempHumDbContext.Set<TempHums>().Where(o => o.Id == id).Select(o => o.Temp).FirstOrDefault();
            return temp;
        }

        // hàm lấy giá trị độ ẩm từ database để hiển thị lên UI
        public async Task<int> getHum(int id)
        {
            var hum = _tempHumDbContext.Set<TempHums>().Where(o => o.Id == id).Select(o => o.Hum).FirstOrDefault();
            return hum;
        }
    }
}
