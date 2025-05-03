using Microsoft.EntityFrameworkCore;
using LMS_Project.Models;
using LMS_Project.ConfigurationClasses;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LMS_Project.Data
{
    public class LMSDbContext : IdentityDbContext<ApplicationUser>  
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }  
        public DbSet<Course> Courses { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }    
        public LMSDbContext(DbContextOptions options): base(options)
        {

        }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LMSDbContext).Assembly);
        }

       
    }
}

