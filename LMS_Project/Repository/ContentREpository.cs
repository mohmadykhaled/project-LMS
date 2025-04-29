using LMS_Project.Data;
using LMS_Project.Interfaces;
using LMS_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Repository
{
    public class ContentREpository : GenericRepostiory<Content>, IContentRepository
    {
        private readonly LMSDbContext context;
        public ContentREpository(LMSDbContext _context) : base(_context)
        {  
            context = _context;
        }

        public async Task<IEnumerable<Content>> GetContentsByCourseIdAsync(int courseId)
        {
          
            return await context.Contents.Where(c => c.CourseId == courseId).ToListAsync();
        }
    }
}
