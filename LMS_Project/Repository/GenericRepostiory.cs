using LMS_Project.Data;
using LMS_Project.Interfaces;
using LMS_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Repository
{
    public class GenericRepostiory<T> : IGenericRepository<T> where T : class
    {
        private readonly LMSDbContext context;
        public GenericRepostiory(LMSDbContext _context)
        {
            context = _context;
        }

        public async Task Add(T entity)
        {
           await  context.AddAsync(entity);
        }

        public async Task Delete(int Id)
        {
            var entity = await context.Set<T>().FindAsync(Id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
            }
        }
           

        public async Task<IEnumerable<T>> GetAll()
        {
            IEnumerable<T> entities = await context.Set<T>().ToListAsync();
            return entities;    
        }

        public async Task<T> GetById(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                return entity;
            }
            else
            { 
                return null;
            }
        }

        public async Task Save(T entity)
        {
             await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            context.Update(entity);
        }
    }

}
