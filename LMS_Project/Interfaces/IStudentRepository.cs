using LMS_Project.Models;
namespace LMS_Project.Interfaces
{
    public interface  IStudentRepository : IGenericRepository<Student>
    {
        Task<Student> GetByApplicationUserId(string applicationUserId);
        Task<int> Countasync() ;
    }

}
