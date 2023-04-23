using DBRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DBRepository.Factories
{
	public class RepositoryContextFactory : BaseRepositoryContextFactory, IRepositoryContextFactory
    {
        public RepositoryContextFactory(string connectionString) : base (connectionString)
        {

		}

		public RepositoryContext CreateDbContext()
		{
			
			var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
			optionsBuilder.UseSqlServer(ConnectionString);

			return new RepositoryContext(optionsBuilder.Options);
		}
    }
}
