using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbcontext;
        public IProductRepository ProductRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

        public UnitOfWork(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            ProductRepository = new ProductRepository(dbcontext);
            CategoryRepository = new CategoryRepository(dbcontext);
            UserRepository = new UserRepository(dbcontext);
        }

        public async Task<int> Complete()
        {
            return await _dbcontext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
