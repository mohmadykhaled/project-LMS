using LMS_Project.Models;

namespace LMS_Project.Interfaces
{
    public interface IStudentCourseRepository :IGenericRepository<StudentCourse>
    {
        Task<IEnumerable<StudentCourse>> GetStudentCoursesByStudentId(int studentId);
        Task<IEnumerable<StudentCourse>> GetStudentCoursesByCourseId(int courseId);
        Task EnrollStudentAsync(int studentId, int courseId);
        Task UnEnrollStudentAsync(int studentId, int courseId);
        Task<bool> IsStudentEnrolledAsync(int studentId, int courseId);
    }
}
