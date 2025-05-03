using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Project.ViewModel
{
    public class LoginUsersViewModel
    {
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        [DataType(DataType.Password)]   
        public string Password { get; set; }
        [Display(Name = "Remember Me !!")]
        public bool RememberMe { get; set; }
    }
}
