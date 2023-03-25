using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Configuration
{
    public class RemoteConfiguration : IEntityTypeConfiguration<Remotes>
    {
         //cấu hình bảng cơ sở dữ liệu tương ứng với đối tượng remote
        public void Configure(EntityTypeBuilder<Remotes> builder)
        {
            builder.ToTable("remote");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.KeyRemote);
        }
    }
}
