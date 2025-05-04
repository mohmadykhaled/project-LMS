namespace LMS_Project.ViewModel
{
    public class InstructorProfileViewModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public List<CourseViewModel> CoursesTaught { get; set; } = new List<CourseViewModel>();
    }
  
}
