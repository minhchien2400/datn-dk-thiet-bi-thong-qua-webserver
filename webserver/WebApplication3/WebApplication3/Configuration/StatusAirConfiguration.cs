using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Configuration
{
    public class StatusAirConfiguration : IEntityTypeConfiguration<StatusAirs>
    {
        //cấu hình bảng cơ sở dữ liệu tương ứng với đối tượng status
        public void Configure(EntityTypeBuilder<StatusAirs> builder)
        {
            builder.ToTable("statusair");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Status);
            builder.Property(a => a.Mode);
            builder.Property(a => a.Speed);
            builder.Property(a => a.Temp);
        }
    }
}
