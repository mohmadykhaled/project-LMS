using System.ComponentModel.DataAnnotations;

namespace LMS_Project.ViewModel
{
    public class GetAllStudentsViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "User Name")]   
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enrollment date is required")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public List<string> Courses { get; set; }
    }
}
