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
            return await _context
                 .Courses
                .Include(c => c.Instructor)
                .ThenInclude(I => I.User)
                .Include(c => c.StudentCourses)
                .AsNoTracking().ToListAsync();
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

        public async Task<Admin> GetByApplicationUserId(string applicationUserId)
        {
           return await _context.Admins.Include(a => a.User)
                .FirstOrDefaultAsync(a => a.ApplicationUserId == applicationUserId);
        }

      
    }
}