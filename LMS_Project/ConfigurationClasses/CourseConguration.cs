using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LMS_Project.Models;

namespace LMS_Project.ConfigurationClasses
{
    public class CourseConguration :IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);
            builder.HasMany(c => c.Materials)
                .WithOne(m => m.Course)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.StudentCourses)
                .WithOne(sc => sc.Course)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
