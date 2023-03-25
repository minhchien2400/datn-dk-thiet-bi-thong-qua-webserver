using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication3.Configuration
{
    public class TempHumConfiguration : IEntityTypeConfiguration<TempHums>
    {
        //cấu hình bảng cơ sở dữ liệu tương ứng với đối tượng TempHum
        public void Configure(EntityTypeBuilder<TempHums> builder)
        {
            builder.ToTable("temphum");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Temp);
            builder.Property(t => t.Hum);
        }
    }
}
