using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApplication3.Data
{
    public class SmartHomeDbContextFactory : IDesignTimeDbContextFactory<SmartHomeDbContext>
    {
        // tạo đối tượng DbContext cho việc truy cập cơ sở dữ liệu
        public SmartHomeDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configurationRoot.GetConnectionString("SmartHomeDatabase");

            var optionBuilder = new DbContextOptionsBuilder<SmartHomeDbContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new SmartHomeDbContext(optionBuilder.Options); //trả về một đối tượng SmartHomeDbContext được khởi tạo bằng cách sử dụng các thông tin cấu hình được đọc từ tệp appsettings.json.
        }
    }
}
