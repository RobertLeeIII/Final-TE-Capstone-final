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
}
