using LMS_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS_Project.ConfigurationClasses
{
    public class InstructorConfiguration :IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(i => i.InstructorId);
            builder.HasMany(i => i.Courses)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    
    
}
