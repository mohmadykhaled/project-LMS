using LMS_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS_Project.ConfigurationClasses
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
       

        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(m => m.MaterialId);

            builder.Property(m => m.Title)
                   .IsRequired()
                   .HasMaxLength(255); 

            builder.Property(m => m.FileUrl)
                   .IsRequired()
                   .HasMaxLength(500); 

            builder.HasOne(m => m.Course)
                   .WithMany(c => c.Materials) 
                   .HasForeignKey(m => m.CourseId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
   
}
