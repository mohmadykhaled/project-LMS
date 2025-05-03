using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LMS_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties for one-to-one relationships
        public Student StudentProfile { get; set; }
        public Instructor InstructorProfile { get; set; }
        public Admin AdminProfile { get; set; }
    }
}