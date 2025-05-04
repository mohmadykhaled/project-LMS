using LMS_Project.Data;
using LMS_Project.Interfaces;
using LMS_Project.Models;
using LMS_Project.Repository;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Repositories
{
    public class InstructorRepository : GenericRepostiory<Instructor>, IInstructorRepository
    {
        private readonly LMSDbContext _context;


        public InstructorRepository(LMSDbContext context) :base(context)    
    {
            this._context = context;
        }

        public async Task<bool> SubmitCourseForApproval(int instructorId, Course course)
        {
            try
            {
                course.InstructorId = instructorId;
                // Set initial state for admin review
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Course>> GetInstructorCourses(int instructorId)
        {
            return await _context.Courses
                .Where(c => c.InstructorId == instructorId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Instructor> GetByApplicationUserId(string applicationUserId)
        {
            return await _context.Instructors
                .Include(i => i.Courses)    
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.ApplicationUserId == applicationUserId);    
        }
    }
}