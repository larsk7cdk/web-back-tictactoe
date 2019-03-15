using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using web_back_tictactoe.web.Models;

namespace web_back_tictactoe.web.Services
{
    public interface IGameInvitationService
    {
        Task<GameInvitationModel> Add(GameInvitationModel gameInvitationModel);
        Task<GameInvitationModel> Get(Guid id);
        Task Update(GameInvitationModel gameInvitationModel);
    }

    public class GameInvitationService : IGameInvitationService
    {
        private static ConcurrentBag<GameInvitationModel> _gameInvitations;

        public GameInvitationService()
        {
            _gameInvitations = new ConcurrentBag<GameInvitationModel>();
        }

        public Task<GameInvitationModel> Add(GameInvitationModel gameInvitationModel)
        {
            gameInvitationModel.Id = Guid.NewGuid();
            _gameInvitations.Add(gameInvitationModel);
            return Task.FromResult(gameInvitationModel);
        }

        public Task Update(GameInvitationModel gameInvitationModel)
        {
            _gameInvitations =
                new ConcurrentBag<GameInvitationModel>(_gameInvitations.Where(x => x.Id != gameInvitationModel.Id))
                {
                    gameInvitationModel
                };
            return Task.CompletedTask;
        }

        public Task<GameInvitationModel> Get(Guid id)
        {
            return Task.FromResult(_gameInvitations.FirstOrDefault(x => x.Id == id));
        }
    }
}