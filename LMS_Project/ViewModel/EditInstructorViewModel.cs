using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS_Project.ViewModel
{
    public class EditInstructorViewModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public string UserName { get; set; }
        public List<string> AvailableCourses { get; set; }
    }
}
