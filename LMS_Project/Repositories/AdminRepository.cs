using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS_Project.Models;
using System.Linq;

namespace LMS_Project.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly LMSDbContext _context;

        public AdminRepository(LMSDbContext context)
        {
            _context = context;
        }

        // User Management
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Course Management
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            return await _context.Courses.FindAsync(courseId);
        }

        public async Task<bool> CreateCourseAsync(Course course)
        {
            try
            {
                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            try
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCourseAsync(int courseId)
        {
            try
            {
                var course = await _context.Courses.FindAsync(courseId);
                if (course != null)
                {
                    _context.Courses.Remove(course);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // System Settings
        public async Task<SystemSettings> GetSystemSettingsAsync()
        {
            return await _context.SystemSettings.FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateSystemSettingsAsync(SystemSettings settings)
        {
            try
            {
                _context.SystemSettings.Update(settings);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Analytics and Reporting
        public async Task<Dictionary<string, int>> GetUserStatisticsAsync()
        {
            var stats = new Dictionary<string, int>
            {
                ["TotalUsers"] = await _context.Users.CountAsync(),
                ["ActiveUsers"] = await _context.Users.Where(u => u.IsActive).CountAsync(),
                ["InactiveUsers"] = await _context.Users.Where(u => !u.IsActive).CountAsync()
            };
            return stats;
        }

        public async Task<Dictionary<string, int>> GetCourseStatisticsAsync()
        {
            var stats = new Dictionary<string, int>
            {
                ["TotalCourses"] = await _context.Courses.CountAsync(),
                ["ActiveCourses"] = await _context.Courses.Where(c => c.IsActive).CountAsync(),
                ["InactiveCourses"] = await _context.Courses.Where(c => !c.IsActive).CountAsync()
            };
            return stats;
        }

        // Role Management
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<bool> AssignRoleToUserAsync(string userId, string roleId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                var role = await _context.Roles.FindAsync(roleId);
                
                if (user != null && role != null)
                {
                    var userRole = new UserRole
                    {
                        UserId = userId,
                        RoleId = roleId
                    };
                    await _context.UserRoles.AddAsync(userRole);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveRoleFromUserAsync(string userId, string roleId)
        {
            try
            {
                var userRole = await _context.UserRoles
                    .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
                
                if (userRole != null)
                {
                    _context.UserRoles.Remove(userRole);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}