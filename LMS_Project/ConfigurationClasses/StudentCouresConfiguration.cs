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

           
            builder.ToTable("StudentCourses");

           
            builder.HasKey(sc => new { sc.StudentId, sc.CourseId });

            
            builder.Property(sc => sc.StudentId)
                   .IsRequired();

            builder.HasOne(sc => sc.Student)
                   .WithMany(student => student.StudentCourses)
                   .HasForeignKey(sc => sc.StudentId)
                   .OnDelete(DeleteBehavior.Cascade); 

            
            builder.Property(sc => sc.CourseId)
                   .IsRequired(); 

            builder.HasOne(sc => sc.Course)
                   .WithMany(course => course.StudentCourses)
                   .HasForeignKey(sc => sc.CourseId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
