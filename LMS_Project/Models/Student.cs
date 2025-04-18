using System.ComponentModel.DataAnnotations;

namespace LMS_Project.Models
{
    public class Student :User
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Enrollment date is required")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }

}
