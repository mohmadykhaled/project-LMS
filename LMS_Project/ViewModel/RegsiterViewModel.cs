using System.ComponentModel.DataAnnotations;

namespace LMS_Project.ViewModel
{
    public class RegsiterViewModel
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]  
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool RememberMe { get; set; }
       
        [Required(ErrorMessage ="User Name is Required")]
        [Display(Name = "User Name")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "User name can only contain letters and numbers.")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Full name can only contain letters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } // Admin, Student, or Instructor

        // Student-specific fields
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        // Instructor-specific field
        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }
    }
}
