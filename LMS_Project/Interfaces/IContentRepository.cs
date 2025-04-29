using LMS_Project.Models;
namespace LMS_Project.Interfaces
{
    public interface IContentRepository : IGenericRepository<Content>
    {
        Task<IEnumerable<Content>> GetContentsByCourseIdAsync(int courseId);
    }
}
