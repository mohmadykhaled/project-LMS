using LMS_Project.Models;

namespace LMS_Project.Interfaces
{
    public interface IInstructorRepository : IGenericRepository<Instructor>
    {
        // Custom methods specific to Instructor
        Task<bool> SubmitCourseForApproval(int instructorId, Course course);
        Task<IEnumerable<Course>> GetInstructorCourses(int instructorId);
        Task<Instructor> GetByApplicationUserId(string applicationUserId);
        Task<List<Instructor>> GetAllwithUser();
        Task<int> CountAsync();
        Task<Instructor> GetByIdIncludeUser(int Id);
    }
}