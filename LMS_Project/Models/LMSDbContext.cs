using Microsoft.EntityFrameworkCore;
using LMS_Project.ConfigurationClasses;
namespace LMS_Project.Models
{
    public class LMSDbContext : DbContext  
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }  
        public DbSet<Course> Courses { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public LMSDbContext(DbContextOptions options): base(options)
        {

        }
      

           protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new InstructorConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConguration());
           // modelBuilder.ApplyConfiguration(new MaterialConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());

        }


    }
}

