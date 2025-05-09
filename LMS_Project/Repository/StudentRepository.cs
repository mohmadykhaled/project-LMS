using LMS_Project.Data;
using LMS_Project.Interfaces;
using LMS_Project.Models;
using LMS_Project.ViewModel;
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
                 .ThenInclude(sc => sc.Course)
                 .ThenInclude(c => c.Instructor)
                .FirstOrDefaultAsync(s => s.ApplicationUserId == applicationUserId);
        }
        public async Task<int> Countasync()
        {
            return await context.Students.CountAsync();
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await  context.Students
                .Include(s => s.User)
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)   
                .AsNoTracking()
                .ToListAsync();
        }
    }
   
}
