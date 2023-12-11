//using Capstone.DAO;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System;

//[Route("controller")]
//[ApiController]
//[Authorize]
//public class PasswordResetController : ControllerBase
//{
//    private readonly IUserDao _userDao;

//    public PasswordResetController(IUserDao userDao)
//    {
//        _userDao = userDao;
//    }

//    [HttpPost("forgot")]
//    public IActionResult ForgotPassword([FromBody] ForgotPasswordRequest request)
//    {
//        if (string.IsNullOrEmpty(request?.Email))
//        {
//            return BadRequest(new { Error = "Email is required" });
//        }

//        var user = _userDao.GetUserByEmailAddress(request.Email);

//        if (user == null)
//        {
//            return NotFound(new { Error = "User not found" });
//        }

//        // Return the user's security question for use in the frontend
//        return Ok(new { SecurityQuestion = user.SecurityQuestion, Message = "Security question retrieved successfully" });
//    }

//    [HttpPost("reset")]
//    public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
//    {
//        try
//        {
//            if (string.IsNullOrEmpty(request?.Email) || string.IsNullOrEmpty(request?.SecurityAnswer) || string.IsNullOrEmpty(request?.NewPassword))
//            {
//                return BadRequest(new { Error = "Email, SecurityAnswer, and NewPassword are required" });
//            }

//            // Find the user by email
//            var user = _userDao.GetUserByEmailAddress(request.Email);

//            if (user == null)
//            {
//                return NotFound(new { Error = "User not found" });
//            }

//            // Check if the provided security answer matches the stored answer
//            if (user.SecurityAnswer != request.SecurityAnswer)
//            {
//                return Unauthorized(new { Error = "Invalid security answer" });
//            }

//            // Hash the new password before storing it
//            user.PasswordHash = HashPassword(request.NewPassword);

//            // Save changes to the database
//            _userDao.UpdateUser(user);

//            return Ok(new { Message = "Password reset successfully" });
//        }
//        catch (Exception)
//        {
//            // Log the exception for troubleshooting
//            return StatusCode(500, new { Error = "Error resetting password" });
//        }
//    }

//    private string HashPassword(string password)
//    {
//        // Implement password hashing logic (e.g., bcrypt)
//        // ...

//        return password; // Replace with actual hashed password
//    }
//}
