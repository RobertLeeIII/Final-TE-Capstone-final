
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class ForgotPasswordRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}

