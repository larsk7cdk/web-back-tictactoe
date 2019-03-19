using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_back_tictactoe.web.Models;

namespace web_back_tictactoe.web.Services
{
    public interface IGameSessionService
    {
        Task<GameSessionModel> GetGameSession(Guid gameSessionId);
        Task<GameSessionModel> CreateGameSession(Guid invitationId, string invitedByEmail, string invitedPlayerEmail);
        Task<GameSessionModel> AddTurn(Guid id, string email, int x, int y);
    }

    public class GameSessionService : IGameSessionService
    {
        private static ConcurrentBag<GameSessionModel> _sessions;
        private readonly IUserService _userService;

        static GameSessionService()
        {
            _sessions = new ConcurrentBag<GameSessionModel>();
        }

        public GameSessionService(IUserService userService)
        {
            _userService = userService;
        }

        public Task<GameSessionModel> GetGameSession(Guid gameSessionId)
        {
            return Task.Run(() => _sessions.FirstOrDefault(x => x.Id == gameSessionId));
        }


        public async Task<GameSessionModel> CreateGameSession(Guid invitationId, string invitedByEmail,
            string invitedPlayerEmail)
        {
            var invitedBy = await _userService.GetUserByEmail(invitedByEmail);
            var invitedPlayer = await _userService.GetUserByEmail(invitedPlayerEmail);

            var session = new GameSessionModel
            {
                User1 = invitedBy,
                User2 = invitedPlayer,
                Id = invitationId,
                ActiveUser = invitedBy
            };

            _sessions.Add(session);
            return session;
        }

        public async Task<GameSessionModel> AddTurn(Guid id, string email, int x, int y)
        {
            var gameSession = _sessions.FirstOrDefault(session => session.Id == id);
            List<TurnModel> turns;
            if (gameSession.Turns != null && gameSession.Turns.Any())
                turns = new List<TurnModel>(gameSession.Turns);
            else
                turns = new List<TurnModel>();

            turns.Add(new TurnModel
            {
                User = await _userService.GetUserByEmail(email),
                X = x,
                Y = y
            });

            if (gameSession.User1?.Email == email)
                gameSession.ActiveUser = gameSession.User2;
            else
                gameSession.ActiveUser = gameSession.User1;

            gameSession.TurnFinished = true;
            _sessions = new ConcurrentBag<GameSessionModel>(_sessions.Where(u => u.Id != id))
            {
                gameSession
            };
            return gameSession;
        }
    }
}