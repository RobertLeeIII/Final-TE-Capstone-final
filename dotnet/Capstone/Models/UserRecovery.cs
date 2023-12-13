namespace Capstone.Models
{
    public class UserRecovery
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
    public class RecoveryQuestion
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
    }
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string SecurityAnswer { get; set; }
        public string NewPassword { get; set; }
    }
}
