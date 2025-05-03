using LMS_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS_Project.ConfigurationClasses
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            
            builder.HasKey(s => s.StudentId);

           
            builder.Property(s => s.ApplicationUserId)
                .IsRequired();

          
            builder.Property(s => s.EnrollmentDate)
                .IsRequired();

            builder.Property(s => s.DateOfBirth)
                .IsRequired();

            
            builder.HasOne(s => s.User)
                .WithOne(u => u.StudentProfile)
                .HasForeignKey<Student>(s => s.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade); 

           
            builder.HasMany(s => s.StudentCourses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

           
            builder.HasIndex(s => s.ApplicationUserId).IsUnique();
        }

      
    }
    
    
}
