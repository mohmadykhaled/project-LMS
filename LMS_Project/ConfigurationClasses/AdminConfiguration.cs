using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LMS_Project.Models;

namespace LMS_Project.ConfigurationClasses
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");

           
            builder.HasKey(a => a.AdminId);

            
            builder.Property(a => a.ApplicationUserId)
                .IsRequired();

            
            builder.HasOne(a => a.User)
                .WithOne(u => u.AdminProfile)
                .HasForeignKey<Admin>(a => a.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}