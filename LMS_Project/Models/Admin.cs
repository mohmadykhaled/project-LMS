using System.ComponentModel.DataAnnotations;

namespace LMS_Project.Models
{
    public class Admin : User 
    {
        [Key]
        public int AdminId { get; set; }
    }
}
