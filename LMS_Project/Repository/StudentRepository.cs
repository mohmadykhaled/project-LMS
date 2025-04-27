using LMS_Project.Data;
using LMS_Project.Interfaces;
using LMS_Project.Models;

namespace LMS_Project.Repository
{
    public class StudentRepository : GenericRepostiory<Student> , IStudentRepository
    {
        private readonly LMSDbContext context;

        public StudentRepository(LMSDbContext _context) : base(_context)
        {
            context = _context;
        }

      
    }
   
}
