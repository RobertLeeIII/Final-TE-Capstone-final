using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class UserRecovery
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        [Required]
        public string Answer { get; set; }
    }
    public class RecoveryQuestion
    {
        public int QuestionId { get; set; }
        [Required]
        public string QuestionText { get; set; }
    }
    public class ResetPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string SecurityAnswer { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 8)]
        public string NewPassword { get; set; }
    }
}
