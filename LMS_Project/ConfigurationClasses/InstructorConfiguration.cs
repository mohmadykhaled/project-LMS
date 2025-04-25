using LMS_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS_Project.ConfigurationClasses
{
    public class InstructorConfiguration :IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructors");

            builder.HasKey(i => i.InstructorId);

            builder.Property(i => i.HireDate)
                .IsRequired();

            builder.Property(i => i.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.Password)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(i => i.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.FullName)
                .HasMaxLength(100);

          
            builder.HasMany(i => i.Courses)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(i => i.Email).IsUnique();
        }
    }
    
    
}
