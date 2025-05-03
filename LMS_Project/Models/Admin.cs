using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Project.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string ApplicationUserId { get; set; }

      
        public ApplicationUser User { get; set; }
    }
}