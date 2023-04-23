using DBRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Threading.Tasks;

namespace DBRepository.Repositories
{
    public class IdentityRepository : BaseRepository, IIdentityRepository
    {
		public IdentityRepository(RepositoryContext context) : base(context) { }

		public async Task<User> GetUser(string userName)
		{
			return await Context.Users.FirstOrDefaultAsync(u => u.Login == userName);
		}
	}
}
