using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Project.Models
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string ApplicationUserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public List<Course> Courses { get; set; }

        
        public ApplicationUser User { get; set; }
    }
}