using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepository.Factories
{
    public abstract class BaseRepositoryContextFactory
    {
        protected string ConnectionString { get; }
        public BaseRepositoryContextFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
