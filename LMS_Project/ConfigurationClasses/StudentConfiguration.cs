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

            builder.Property(s => s.EnrollmentDate)
                .IsRequired();

            builder.Property(s => s.DateOfBirth)
                .IsRequired();

           
            builder.Property(s => s.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Password)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.FullName)
                .HasMaxLength(100);


            builder.HasMany(s => s.StudentCourses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => s.Email).IsUnique();
        }

      
    }
    
    
}
