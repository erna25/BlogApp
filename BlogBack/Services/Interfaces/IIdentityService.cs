using Models;
using System.Threading.Tasks;

namespace BlogBack.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<User> GetUser(string userName);
    }
}
