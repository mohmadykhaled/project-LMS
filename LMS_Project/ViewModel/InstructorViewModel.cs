using System.ComponentModel.DataAnnotations;

namespace LMS_Project.ViewModel
{
    public class InstructorViewModel
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


        [Required(ErrorMessage = "User Name is Required")]
        [Display(Name = "User Name")]
        [MinLength(5)]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Full name can only contain letters and spaces")]
        public string FullName { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;

    }
}
