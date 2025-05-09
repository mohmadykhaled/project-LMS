using LMS_Project.Models;

namespace LMS_Project.ViewModel
{
    public class IstnructorListViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<string> Courses { get; set; }
        public DateTime HireDate { get; set; }
    }
}
