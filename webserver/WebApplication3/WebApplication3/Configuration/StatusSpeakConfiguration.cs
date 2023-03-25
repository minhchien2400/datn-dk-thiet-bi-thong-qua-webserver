using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Configuration
{
    public class StatusSpeakConfiguration : IEntityTypeConfiguration<StatusSpeaks>
    {
        //cấu hình bảng cơ sở dữ liệu tương ứng với đối tượng stautus speak
        public void Configure(EntityTypeBuilder<StatusSpeaks> builder)
        {
            builder.ToTable("statusspeak");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Status);
            builder.Property(s => s.PlayPause);
            builder.Property(s => s.Volume);
        }
    }
}
