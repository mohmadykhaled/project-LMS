using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Project.Models
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
      
        [Required(ErrorMessage = "NOT Found! ")]
        public string FileUrl { get; set; }
        [ForeignKey("Course")]
        public int? CourseId { get; set; }
        public Course Course { get; set; }

    }
}
