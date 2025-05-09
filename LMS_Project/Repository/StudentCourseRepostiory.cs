using LMS_Project.Data;
using LMS_Project.Interfaces;
using LMS_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Repository
{
    public class StudentCourseRepostiory : GenericRepostiory<StudentCourse>, IStudentCourseRepository
    {
        private readonly LMSDbContext context;

        public StudentCourseRepostiory(LMSDbContext _context) : base(_context)
        {
            this.context = _context;
        }

        public async Task EnrollStudentAsync(int studentId, int courseId)
        {
            
            var Enrollment = await context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.CourseId == courseId && sc.StudentId == studentId);

            if (Enrollment == null)
            {
                Enrollment = new StudentCourse
                {
                    StudentId = studentId,
                    CourseId = courseId
                };

                // أضف الكائن الجديد إلى قاعدة البيانات
                await context.StudentCourses.AddAsync(Enrollment);
                await context.SaveChangesAsync();
            }
        }
 

        public async Task<IEnumerable<StudentCourse>> GetStudentCoursesByStudentId(int studentId)
        {
            return await context.StudentCourses
                .Where(SC => SC.StudentId == studentId)
                .Include(sc =>sc.Course)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentCourse>> GetStudentCoursesByCourseId(int courseId)
        {
            return await  context.StudentCourses
                                 .Where(sc => sc.CourseId == courseId)
                                 .Include(sc => sc.Student)
                                 .AsNoTracking()
                                 .ToListAsync();
        }
        public async Task  UnEnrollStudentAsync(int studentId, int courseId)
        {
            var Enrollment = await context.StudentCourses.FirstOrDefaultAsync(sc => sc.CourseId == courseId && sc.StudentId == studentId);
            if (Enrollment != null)
            {
                context.StudentCourses.Remove(Enrollment);
            }
        }

        public async Task<bool> IsStudentEnrolledAsync(int studentId, int courseId)
        {
           
            return await context.StudentCourses
                .AnyAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);
        }
    }
}
