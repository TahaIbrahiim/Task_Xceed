using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        private readonly AppDbContext _dbcontext;

        public BaseRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Product))
                return (IEnumerable<T>)await _dbcontext.Products.Include(E => E.Category).ToListAsync();
            else
                return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
            => await _dbcontext.Set<T>().FindAsync(id);

        public async Task Add(T entity)
            => await _dbcontext.Set<T>().AddAsync(entity);

        public void Delete(T entity)
            => _dbcontext.Set<T>().Remove(entity);

        public void Edit(T entity)
           => _dbcontext.Set<T>().Update(entity);

    }
}
