using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;

[Route("[controller]")]
[ApiController]
public class PasswordResetController : ControllerBase
{
    private readonly IUserDao _userDao;
    private readonly IRecoveryQuestionDao _recoveryQuestionDao;

    public PasswordResetController(IUserDao userDao, IRecoveryQuestionDao recoveryQuestionDao)
    {
        _userDao = userDao;
        _recoveryQuestionDao = recoveryQuestionDao;
    }
    [HttpGet("forgot/{email}")]
    public ActionResult<string> PasswordRecoveryQuestion(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest(new { Error = "Email is required" });
        }
        string question = _recoveryQuestionDao.GetQuestionText(email);

        if (question == null)
        {
            return NotFound(new { Error = "Question not found" });
        }

        return question;

    }
    [HttpPost("forgot")]
    public ActionResult<bool> PasswordRecoveryAnswer(ForgotPasswordRequest fpr)
    {
        if ((string.IsNullOrEmpty(fpr.Email)) || (string.IsNullOrEmpty(fpr.Answer)))
        {
            return BadRequest(new { Error = "Email and Answer are required" });
        }
        string match = _recoveryQuestionDao.GetAnswer(fpr.Email);

        if (match == null)
        {
            return NotFound(new { Error = "Answer not found" });
        }
        if (match != fpr.Answer)
        {
            return BadRequest(new { Error = "Answer is incorrect" });
        }
       
        return Ok(match == fpr.Answer);
    }
    [HttpPut("reset/{email}")]
    public ActionResult<bool> UpdateUserPassword(UpdateUserDTO uud)
    {
       if(uud.ConfirmPassword == uud.Password)
        {
            try
            {
                _userDao.UpdatePassword(uud.Email, uud.Password);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
            return Ok(true);
    }

    //[HttpPost("forgot")]
    //public IActionResult ForgotPassword([Freset/:emailromBody] ForgotPasswordRequest request)
    //{
    //    if (string.IsNullOrEmpty(request?.Email))
    //    {
    //        return BadRequest(new { Error = "Email is required" });
    //    }

    //    User user = _userDao.GetUserByEmailAddress(request.Email);

    //    if (user == null)
    //    {
    //        return NotFound(new { Error = "User not found" });
    //    }

    //    // Retrieve the user's security question from the user_recovery table
    //    UserRecovery userRecovery = _userDao.GetUserRecovery(user.UserId);
    //    if (userRecovery == null)
    //    {
    //        return NotFound(new { Error = "Security question not found for the user" });
    //    }

    //    // Retrieve the text of the security question from the recovery_questions table
    //    string question = _recoveryQuestionDao.GetQuestionText(userRecovery.QuestionId);

    //    // Return the user's security question for use in the frontend
    //    return Ok(new { SecurityQuestion = question, Message = "Security question retrieved successfully" });
    //}

    //[HttpPost("reset")]
    //public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
    //{
    //    try
    //    {
    //        if (string.IsNullOrEmpty(request?.Email) || string.IsNullOrEmpty(request?.SecurityAnswer) || string.IsNullOrEmpty(request?.NewPassword))
    //        {
    //            return BadRequest(new { Error = "Email, SecurityAnswer, and NewPassword are required" });
    //        }

    //        // Find the user by email
    //        User user = _userDao.GetUserByEmailAddress(request.Email);

    //        if (user == null)
    //        {
    //            return NotFound(new { Error = "User not found" });
    //        }

    //        // Retrieve the user's security question from the user_recovery table
    //        UserRecovery userRecovery = _userDao.GetUserRecovery(user.UserId);
    //        if (userRecovery == null)
    //        {
    //            return NotFound(new { Error = "Security question not found for the user" });
    //        }

    //        // Check if the provided security answer matches the stored answer
    //        if (userRecovery.Answer != request.SecurityAnswer)
    //        {
    //            return Unauthorized(new { Error = "Invalid security answer" });
    //        }

    //        // Hash the new password before storing it
    //        string hashedPassword = HashPassword(request.NewPassword);
    //        user.PasswordHash = hashedPassword;

    //        // Save changes to the database
    //        _userDao.UpdateUser(user);

    //        return Ok(new { Message = "Password reset successfully" });
    //    }
    //    catch (Exception)
    //    {
    //        // Log the exception for troubleshooting
    //        return StatusCode(500, new { Error = "Error resetting password" });
    //    }
    //}

    //private string HashPassword(string password)
    //{
    //    // Implement a secure password hashing mechanism (e.g., bcrypt) here
    //    // ...

    //    return password; // Replace with actual hashed password
    //}
}
