using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Configuration
{
    public class FireAlarmConfiguration : IEntityTypeConfiguration<FireAlarms>
    {
        //cấu hình bảng cơ sở dữ liệu tương ứng với đối tượng FireAlarms
        public void Configure(EntityTypeBuilder<FireAlarms> builder)
        {
            builder.ToTable("firealarm");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.KeyFire);
        }
    }
}
