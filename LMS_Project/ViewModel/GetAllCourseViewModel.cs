namespace LMS_Project.ViewModel
{
    public class GetAllCourseViewModel
    {
        public int Id { get; set; }    
        public string CourseName { get; set; }
        public string Title { get; set; }     
        public float Price { get; set; }
        public string? ImageUrl { get; set; }
        public string InstructorName { get; set; } = "Not Assigned";
        public int StudentCount { get; set; }       
       
    }
}
