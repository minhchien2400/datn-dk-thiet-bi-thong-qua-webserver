using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Configuration
{
    public class StatusBulbConfiguration : IEntityTypeConfiguration<StatusBulbs>
    {
        //cấu hình bảng cơ sở dữ liệu tương ứng với đối tượng statusbulb
        public void Configure(EntityTypeBuilder<StatusBulbs> builder)
        {
            builder.ToTable("statusbulb");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Status);
            builder.Property(s => s.Color);
        }
    }
}
