using System.ComponentModel.DataAnnotations;

namespace LMS_Project.ViewModel
{
    public class CreateCourseViewModel
    {
        public int? Id { get; set; } 
        [Required(ErrorMessage = "Course Name is required")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Price is required")]
        public float Price { get; set; }

        public string? ImageUrl { get; set; }

    }
}
