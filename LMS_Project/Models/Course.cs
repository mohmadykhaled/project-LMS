using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Project.Models
{
    public class Course 
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; } 
        public string Description { get; set; }
        [ForeignKey("Instructor")]
        public int? InstructorId { get; set; }   
        public Instructor Instructor { get; set; }
        public List<Material> Materials { get; set; } 
        public List<StudentCourse> StudentCourses { get; set; }
    }
}
