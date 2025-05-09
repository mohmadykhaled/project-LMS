using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace LMS_Project.ViewModel
{
    public class AssignInstructorViewModel
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please select an instructor")]
        public int? InstructorId { get; set; }

        [Display(Name = "Course Name")]
        public string CourseName { get; set; } = string.Empty;

        [Display(Name = "Available Instructors")]
        public List<SelectListItem> Instructors { get; set; } = new();
    }
}
