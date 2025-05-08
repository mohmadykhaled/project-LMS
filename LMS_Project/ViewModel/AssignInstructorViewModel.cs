using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace LMS_Project.ViewModel
{
    public class AssignInstructorViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        [Required]
        public int InstructorId { get; set; }

        public List<SelectListItem> Instructors { get; set; }

    }
}
