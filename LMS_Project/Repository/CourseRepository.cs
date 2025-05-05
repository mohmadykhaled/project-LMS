using LMS_Project.Interfaces;
using LMS_Project.Models;
using LMS_Project.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Repository
{
    public class CourseRepository : GenericRepostiory<Course>, ICourseRepository
    {
        private readonly LMSDbContext _context;

        public CourseRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetCoursesByInstructorIdAsync(int instructorId)
            => await _context.Courses
                .Where(c => c.InstructorId == instructorId)
                .AsNoTracking()
                .ToListAsync();

        public async Task<IEnumerable<Course>> SearchCoursesAsync(string keyword)
            => await _context.Courses
                .Where(c => c.Title.Contains(keyword) ||
                           c.Description.Contains(keyword) ||
                           c.CourseName.Contains(keyword))
                 .AsNoTracking()
                .ToListAsync();

        public async Task<Course> GetCourseWithDetailsAsync(int courseId)
            => await _context.Courses
                .Include(c => c.Contents)
                .Include(c => c.Materials)
                .Include(c => c.Instructor)
                .Include(c => c.StudentCourses)
                 .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourseId == courseId);
        public async Task<IEnumerable<Course>> GetCourseswithInstructor()
            => await _context.Courses.Include(c => c.Instructor)
                .ThenInclude(i => i.User)
                .AsNoTracking()
                .ToListAsync(); 

        public async Task<IEnumerable<Course>> GetStudentCoursesAsync(int studentId)
            => await _context.Courses
                .Where(c => c.StudentCourses.Any(sc => sc.StudentId == studentId))
                .AsNoTracking()
                .ToListAsync();

        public async Task AddMaterialToCourseAsync(int courseId, Material material)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                course.Materials ??= new List<Material>();
                course.Materials.Add(material);
              
            }
        }

        public async Task AddContentToCourseAsync(int courseId, Content content)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                course.Contents ??= new List<Content>();
                course.Contents.Add(content);
                
            }
        }
    }
}
