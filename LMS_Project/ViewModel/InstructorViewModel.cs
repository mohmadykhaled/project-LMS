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
        [StringLength(100, MinimumLength = 5, ErrorMessage = "User Name must be at least 5 characters long.")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;

    }
}
