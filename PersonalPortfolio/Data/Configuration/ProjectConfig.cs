using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalPortfolio.Models;

namespace PersonalPortfolio.Data.Configuration
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(255);
            builder.Property(x => x.ImageUrl).HasMaxLength(255);
            builder.Property(x => x.Description).HasMaxLength(1000);
        }
    }
}
