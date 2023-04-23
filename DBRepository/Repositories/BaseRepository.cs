using DBRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DBRepository.Repositories
{
    public abstract class BaseRepository
    {        
        protected RepositoryContext Context { get; }
        public BaseRepository(RepositoryContext context)
        {
            Context = context;
        }
    }
}
