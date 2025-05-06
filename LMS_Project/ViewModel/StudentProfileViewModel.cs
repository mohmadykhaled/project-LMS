namespace LMS_Project.ViewModel
{
    public class StudentProfileViewModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<CourseViewModel> EnrolledCourses { get; set; } 
    }

    public class CourseViewModel
    {
        public string CourseName { get; set; }
        public string Description { get; set; }
        public string InstructorFullName { get; set; }
    }
}
