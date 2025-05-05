using System.ComponentModel.DataAnnotations;

namespace LMS_Project.ViewModel
{
    public class CreateCourseViewModel
    {
        [Required(ErrorMessage = "Course Name is required")]
        public string CourseName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public float Price { get; set; }

        public string? ImageUrl { get; set; }

    }
}
