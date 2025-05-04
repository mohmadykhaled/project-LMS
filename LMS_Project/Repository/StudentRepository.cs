using LMS_Project.Data;
using LMS_Project.Interfaces;
using LMS_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Repository
{
    public class StudentRepository : GenericRepostiory<Student> , IStudentRepository
    {
        private readonly LMSDbContext context;

        public StudentRepository(LMSDbContext _context) : base(_context)
        {
            context = _context;
        }

        public async Task<Student> GetByApplicationUserId(string applicationUserId)
        {
           return await context.Students
                .Include(s => s.StudentCourses) 
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.ApplicationUserId == applicationUserId);
        }
    }
   
} 
