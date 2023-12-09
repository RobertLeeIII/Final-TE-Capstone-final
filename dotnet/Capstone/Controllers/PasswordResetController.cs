using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Capstone.Security;
using Microsoft.AspNetCore.DataProtection;

[ApiController]
[Route("controller")]
public class PasswordResetController : ControllerBase
{
    private readonly string JwtSecret = "your-secret-key";
    private const string EmailSender = "plannerpotluck@gmail.com";
    private const string EmailSenderPassword = "C#C#DotNet";

    [HttpPost("forgot")]
    public IActionResult ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        if (string.IsNullOrEmpty(request?.Email))
        {
            return BadRequest("Email is required");
        }

        var token = GenerateJwtToken(request.Email);
        var emailSent = SendResetEmail(request.Email, token);

        if (emailSent)
        {
            return Ok(new { Token = token, Message = "Email sent successfully" });
        }

        return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = "Error sending email" });
    }

    [HttpPost("reset")]
    public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request?.Token) || string.IsNullOrEmpty(request?.Password) || string.IsNullOrEmpty(request?.PasswordConfirm))
            {
                return BadRequest("Token, Password, and PasswordConfirm are required");
            }

            var email = ValidateJwtToken(request.Token);

            if (email == null)
            {
                return Unauthorized("Invalid token");
            }

            // TODO: Implement password reset logic here (update the password in your database, for example)

            return Ok(new { Message = "Password reset successfully" });
        }
        catch (SecurityTokenExpiredException)
        {
            return BadRequest("Token has expired");
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = "Error resetting password" });
        }
    }

    private string GenerateJwtToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = System.Text.Encoding.ASCII.GetBytes(JwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("email", email) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = System.Text.Encoding.ASCII.GetBytes(JwtSecret);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        return jwtToken?.Claims?.FirstOrDefault(x => x.Type == "email")?.Value;
    }

    private bool SendResetEmail(string email, string token)
    {
        try
        {
            var fromAddress = new MailAddress(EmailSender, "Your Name");
            var toAddress = new MailAddress(email, "User Name");
            const string subject = "Password Reset";
            var body = $"Click the following link to reset your password: http://localhost:8080/reset/{token}";

            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com", // Update with your SMTP server
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, EmailSenderPassword)
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                    return true;
                }
            }
        }
        catch (Exception)
        {
            // Log or handle the exception
            return false;
        }
    }
}

public class ForgotPasswordRequest
{
    public string Email { get; set; }
}

public class ResetPasswordRequest
{
    public string Token { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
}