using System.Threading.Tasks;
using web_back_tictactoe.web.Models;

namespace web_back_tictactoe.web.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserModel userModel);
    }

    public class UserService : IUserService
    {
        public Task<bool> RegisterUser(UserModel userModel)
        {
            return Task.FromResult(true);
        }
    }
}