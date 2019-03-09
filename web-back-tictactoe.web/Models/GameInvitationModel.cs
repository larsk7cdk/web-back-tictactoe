using System;

namespace web_back_tictactoe.web.Models
{
    public class GameInvitationModel
    {
        public Guid Id { get; set; }
        public string EmailTo { get; set; }
        public string InvitedBy { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime ConfirmationDate { get; set; }
    }
}