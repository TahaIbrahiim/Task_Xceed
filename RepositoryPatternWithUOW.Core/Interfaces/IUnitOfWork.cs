using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository ProductRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        

        Task<int> Complete();
    }
}
