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

           
            builder.Property(i => i.ApplicationUserId)
                .IsRequired();

           
            builder.Property(i => i.HireDate)
                .IsRequired();

           
            builder.HasOne(i => i.User)
                .WithOne(u => u.InstructorProfile)
                .HasForeignKey<Instructor>(i => i.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade); 

           
            builder.HasMany(i => i.Courses)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => s.ApplicationUserId).IsUnique();

        }
    }
    
    
}
