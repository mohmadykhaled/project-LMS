using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Project.Models
{
    public class Course 
    {
        [Key]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Course Name is required")]
        public string CourseName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public float Price { get; set; }  
        public string? ImageUrl { get; set; }    

        [ForeignKey("Instructor")]
        public int? InstructorId { get; set; }   
        public Instructor Instructor { get; set; }
        public List<Material> Materials { get; set; } 
        public List<StudentCourse> StudentCourses { get; set; }
        public List<Content> Contents { get; set; }
    }
}
