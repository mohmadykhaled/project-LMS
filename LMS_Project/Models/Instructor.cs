using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LMS_Project.Models
{
    public class Instructor : User
    {
        [Key]
        public int InstructorId { get; set; }
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        public List<Course> Courses { get; set; }
    }
}
