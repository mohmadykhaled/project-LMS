using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LMS_Project.Models;
using System.Reflection.Emit;

namespace LMS_Project.ConfigurationClasses
{
    public class StudentCouresConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {

            builder.HasKey(sc => new { sc.StudentId, sc.CourseId });
            builder.ToTable("StudentCourses");
        }
    }
}
