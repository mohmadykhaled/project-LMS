using System.Collections.Generic;
using System.Threading.Tasks;
using LMS_Project.Interfaces;
using LMS_Project.Models;

namespace LMS_Project.Repositories
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        // User Management
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);

        // Course Management
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseByIdAsync(int courseId);
        Task<bool> CreateCourseAsync(Course course);
        Task<bool> UpdateCourseAsync(Course course);
        Task<bool> DeleteCourseAsync(int courseId);

        // Analytics and Reporting
        Task<Dictionary<string, int>> GetUserStatisticsAsync();
        Task<Dictionary<string, int>> GetCourseStatisticsAsync();

        // Role Management
        Task<bool> AssignRoleToUserAsync(string userId, string roleName);
        Task<bool> RemoveRoleFromUserAsync(string userId, string roleName);
        Task<Admin> GetByApplicationUserId(string applicationUserId);
        
    }

}