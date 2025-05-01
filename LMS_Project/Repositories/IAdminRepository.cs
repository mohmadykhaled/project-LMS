using System.Collections.Generic;
using System.Threading.Tasks;
using LMS_Project.Models;

namespace LMS_Project.Repositories
{
    public interface IAdminRepository
    {
        // User Management
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string userId);
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(string userId);
        
        // Course Management
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseByIdAsync(int courseId);
        Task<bool> CreateCourseAsync(Course course);
        Task<bool> UpdateCourseAsync(Course course);
        Task<bool> DeleteCourseAsync(int courseId);
        
        // System Settings
        Task<SystemSettings> GetSystemSettingsAsync();
        Task<bool> UpdateSystemSettingsAsync(SystemSettings settings);
        
        // Analytics and Reporting
        Task<Dictionary<string, int>> GetUserStatisticsAsync();
        Task<Dictionary<string, int>> GetCourseStatisticsAsync();
        
        // Role Management
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<bool> AssignRoleToUserAsync(string userId, string roleId);
        Task<bool> RemoveRoleFromUserAsync(string userId, string roleId);
    }
}