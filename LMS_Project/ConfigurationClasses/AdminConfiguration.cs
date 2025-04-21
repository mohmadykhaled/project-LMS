using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LMS_Project.Models;


namespace LMS_Project.ConfigurationClasses
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.AdminId);
            builder.ToTable("Admins");
        }
    }
    
    
}
