using System.ComponentModel.DataAnnotations;

namespace LMS_Project.ViewModel
{
    public class ContactForm
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Message cannot be empty.")]
        [StringLength(1000)]
        public string Message { get; set; }
    }
}
