using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Project.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string ApplicationUserId { get; set; }

        [Required(ErrorMessage = "Enrollment date is required")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }

       
        public ApplicationUser User { get; set; }
    }
}