using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserDao userDao;

        public LoginController(ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher, IUserDao userDao)
        {
            this.tokenGenerator = tokenGenerator;
            this.passwordHasher = passwordHasher;
            this.userDao = userDao;
        }

        [HttpPost]
        public IActionResult Authenticate(LoginUser userParam)
        {
            // Default to bad username/password message
            IActionResult result = Unauthorized(new { message = "Username or password is incorrect." });

            User user;
            // Get the user by username
            try
            { 
                user = userDao.GetUserByUsername(userParam.Username);
            }
            catch (DaoException)
            {
                // return default Unauthorized message instead of indicating a specific error
                return result;
            }

            // If we found a user and the password hash matches
            if (user != null && passwordHasher.VerifyHashMatch(user.PasswordHash, userParam.Password, user.Salt))
            {
                // Create an authentication token
                string token = tokenGenerator.GenerateToken(user.UserId, user.Username, user.Role);

                // Create a ReturnUser object to return to the client
                LoginResponse retUser = new LoginResponse() { User = new ReturnUser() { UserId = user.UserId, Username = user.Username, Role = user.Role }, Token = token };

                // Switch to 200 OK
                result = Ok(retUser);
            }

            return result;
        }

        [HttpPost("/register")]
        public IActionResult Register(RegisterUser userParam)
        {
            // Default generic error message
            const string ErrorMessage = "An error occurred and user was not created.";

            IActionResult result = BadRequest(new { message = ErrorMessage });

            // is username already taken?
            try
            {
                User existingUser = userDao.GetUserByUsername(userParam.Username);
                if (existingUser != null)
                {
                    return Conflict(new { message = "Username already taken. Please choose a different username." });
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }

            // create new user
            User newUser;
            try
            {
                newUser = userDao.CreateUser(userParam.Email, userParam.Username, userParam.Password, userParam.Role, userParam.DietaryRestriction);
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }

            if (newUser != null)
            {
                //SEARCH FOR ANONYMOUS INVITATIONS
                IList<int> invitedPotlucks = userDao.FindInvitationsByEmail(newUser.Email);
                if (invitedPotlucks.Count > 0) //They had anonymous invitations, so insert into 
                {
                    foreach(int potluckId in invitedPotlucks)
                    {
                        userDao.SendRegisteredInvitation(potluckId, newUser.UserId);    //NOW THEIR NEW ACCOUNT IS LINKED TO THE ANONYMOUS INVITES
                    }
                }
                // Create a ReturnUser object to return to the client
                ReturnUser returnUser = new ReturnUser() { UserId = newUser.UserId, Username = newUser.Username, Role = newUser.Role};

                result = Created("/login", returnUser);
            }

            return result;
        }
    }
}
