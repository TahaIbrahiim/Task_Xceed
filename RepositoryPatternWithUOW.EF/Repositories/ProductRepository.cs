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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _dbcontext;

        public ProductRepository(AppDbContext dbcontext) : base(dbcontext) 
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {

            return await _dbcontext.Products.Where(E => E.CreationDate < DateTime.Now).Include(E => E.Category).ToListAsync();

        }
    }
}
