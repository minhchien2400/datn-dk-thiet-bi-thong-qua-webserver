using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication3.Configuration;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class SmartHomeDbContext : DbContext
    {
        public SmartHomeDbContext(DbContextOptions options) : base(options)
        {
        }

// cấu hình các liên kết giữa các đối tượng và cơ sở dữ liệu.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RemoteConfiguration());
            modelBuilder.ApplyConfiguration(new TempHumConfiguration());
            modelBuilder.ApplyConfiguration(new FireAlarmConfiguration());
            modelBuilder.ApplyConfiguration(new StatusBulbConfiguration());
            modelBuilder.ApplyConfiguration(new StatusSpeakConfiguration());
            modelBuilder.ApplyConfiguration(new StatusAirConfiguration());
        }
// cho phép truy xuất đến các bản ghi trong các bảng tương ứng và thực hiện các thao tác trên chúng.
        public DbSet<Remotes> Remote { get; set; }
        public DbSet<TempHums> TempHums { get; set; }
        public DbSet<FireAlarms> FireAlarms { get; set; }
        public DbSet<StatusBulbs> StatusBulbs { get; set; }
        public DbSet<StatusSpeaks> StatusSpeaks { get; set; }
        public DbSet<StatusAirs> StatusAirs { get; set; }
    }
}