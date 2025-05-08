using System.Collections.Generic;
using System.Threading.Tasks;
using LMS_Project.Models;

namespace LMS_Project.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<IEnumerable<Course>> GetCoursesByInstructorIdAsync(int instructorId);
        Task<IEnumerable<Course>> SearchCoursesAsync(string keyword);
        Task<Course> GetCourseWithDetailsAsync(int courseId);
        Task<IEnumerable<Course>> GetStudentCoursesAsync(int studentId);
        Task AddMaterialToCourseAsync(int courseId, Material material);
        Task AddContentToCourseAsync(int courseId, Content content);
        Task<IEnumerable<Course>> GetCourseswithInstructor();
        Task<int> CountAsync();
    }
}
