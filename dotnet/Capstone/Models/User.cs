using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

namespace Capstone.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public string Salt { get; set; }
        public string Role { get; set; }
        public bool DietaryRestriction { get; set; }

    }

    public class InviteUser
    {
        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }

    /// <summary>
    /// Model of user data to return upon successful login
    /// </summary>
    public class ReturnUser
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        public string Role { get; set; }

    }

    /// <summary>
    /// Model to return upon successful login (user data + token)
    /// </summary>
    public class LoginResponse
    {
        public ReturnUser User { get; set; }
        public string Token { get; set; }
    }

    /// <summary>
    /// Model to accept login parameters
    /// </summary>
    public class LoginUser
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// Model to accept registration parameters
    /// </summary>
    public class RegisterUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords do not match.")]
        [StringLength(8, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage ="Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public bool DietaryRestriction { get; set; }
        [Required]
        public string SecurityQuestion { get; set; }
        [Required]
        public string SecurityAnswer { get; set; }
    }
    public class UpdateUserDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        [Compare("ConfirmPassword", ErrorMessage = "Passwords do not match.")]
        [StringLength(8, MinimumLength = 8)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
