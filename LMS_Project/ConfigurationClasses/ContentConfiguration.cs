using LMS_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS_Project.ConfigurationClasses
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.ToTable("Contents");

            
            builder.HasKey(c => c.Id);

            
            builder.Property(c => c.Title)
                   .IsRequired()
                   .HasMaxLength(200); 

            builder.Property(c => c.URL)
                   .IsRequired()
                   .HasMaxLength(200); 

            
            builder.Property(c => c.CourseId)
                   .IsRequired(false); 

            builder.HasOne(c => c.course)
                   .WithMany(course => course.Contents) 
                   .HasForeignKey(c => c.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
