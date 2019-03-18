using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_back_tictactoe.web.Models;

namespace web_back_tictactoe.web.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserModel userModel);
        Task<UserModel> GetUserByEmail(string email);
        Task UpdateUser(UserModel userModel);
        Task<IEnumerable<UserModel>> GetTopUsers(int numberOfUsers);
    }

    public class UserService : IUserService
    {
        private static ConcurrentBag<UserModel> _userStore = new ConcurrentBag<UserModel>();

        static UserService()
        {
            //_userStore = new ConcurrentBag<UserModel>();
        }

        public Task<bool> RegisterUser(UserModel userModel)
        {
            _userStore.Add(userModel);
            return Task.FromResult(true);
        }

        public Task<UserModel> GetUserByEmail(string email)
        {
            return Task.FromResult(_userStore.FirstOrDefault(u => u.Email == email));
        }

        public Task UpdateUser(UserModel userModel)
        {
            _userStore = new ConcurrentBag<UserModel>(_userStore.Where(u => u.Email != userModel.Email))
            {
                userModel
            };

            return Task.CompletedTask;
        }

        public Task<IEnumerable<UserModel>> GetTopUsers(int numberOfUsers) =>
            Task.Run(() => _userStore.OrderBy(x => x.Score).Take(numberOfUsers));
    }
}