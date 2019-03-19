using System;

namespace web_back_tictactoe.web.Models
{
    public class TurnModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserModel User { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}