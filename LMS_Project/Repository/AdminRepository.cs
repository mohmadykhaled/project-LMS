using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS_Project.Data;
using LMS_Project.Interfaces;
using LMS_Project.Models;
using LMS_Project.Repositories;
using LMS_Project.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Services
{
    public class AdminRepository : GenericRepostiory<Admin>, IAdminRepository
    {
        private readonly LMSDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminRepository(LMSDbContext context, UserManager<ApplicationUser> userManager)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // User Management
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.AsNoTracking().ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;
            return await _userManager.FindByIdAsync(userId);
        }

        // Course Management
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.AsNoTracking().ToListAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            return await _context.Courses.FindAsync(courseId);
        }

        public async Task<bool> CreateCourseAsync(Course course)
        {
            if (course == null) return false;

            await _context.Courses.AddAsync(course);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            if (course == null) return false;

            _context.Courses.Update(course);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCourseAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null) return false;

            _context.Courses.Remove(course);
            return await _context.SaveChangesAsync() > 0;
        }

        // Analytics and Reporting
        public async Task<Dictionary<string, int>> GetUserStatisticsAsync()
        {
            var adminCountTask = _userManager.GetUsersInRoleAsync("Admin");
            var studentCountTask = _userManager.GetUsersInRoleAsync("Student");
            var instructorCountTask = _userManager.GetUsersInRoleAsync("Instructor");

            await Task.WhenAll(adminCountTask, studentCountTask, instructorCountTask);

            return new Dictionary<string, int>
            {
                { "Admins", adminCountTask.Result.Count },
                { "Students", studentCountTask.Result.Count },
                { "Instructors", instructorCountTask.Result.Count }
            };
        }

        //public async Task<Dictionary<string, int>> GetCourseStatisticsAsync()
        //{
        //    var totalCoursesTask = _context.Courses.CountAsync();
        //    var activeCoursesTask = _context.Courses.CountAsync(c => c.IsActive);

        //    await Task.WhenAll(totalCoursesTask, activeCoursesTask);

        //    return new Dictionary<string, int>
        //    {
        //        { "TotalCourses", totalCoursesTask.Result },
        //        { "ActiveCourses", activeCoursesTask.Result }
        //    };
        //}

        // Role Management
        public async Task<bool> AssignRoleToUserAsync(string userId, string roleName)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleName)) return false;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> RemoveRoleFromUserAsync(string userId, string roleName)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleName)) return false;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public Task<Dictionary<string, int>> GetCourseStatisticsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Admin> GetByApplicationUserId(string applicationUserId)
        {
           return await _context.Admins.Include(a => a.User)
                .FirstOrDefaultAsync(a => a.ApplicationUserId == applicationUserId);
        }
    }
}