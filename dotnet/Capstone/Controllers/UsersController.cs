using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IPotluckDao potluckDao;
        private readonly IDishDao dishDao;
        private readonly IUserDao userDao;

        public UsersController(IPotluckDao potluckDao, IUserDao userDao, IDishDao dishDao)
        {
            this.potluckDao = potluckDao;
            this.userDao = userDao;
            this.dishDao = dishDao;
        }
        [HttpGet("/potlucks/{potluckId}/guestList")]
        public ActionResult<IList<User>> GetUsersByPotluckId(int potluckId)
        {
            try
            {
                User host = userDao.GetHostByPotluckId(potluckId);
                IList<User> output = userDao.GetUsersByPotluckId(potluckId);
                //TODO: Finish This
                return Ok(output);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("/potlucks/{potluckId}/guestList")]
        public ActionResult<IList<string>> SendInvitations(IList<string> invitations, int potluckId)
        {
            int total = invitations.Count;
            try
            {
                IList<string> output = new List<string>();
                foreach (string email in invitations)
                {
                    InviteUser temp = userDao.GetUserByEmail(email);
                    if (temp.Email != null)
                    {
                        userDao.SendRegisteredInvitation(potluckId, temp.UserId); //if they have an account, send that way
                    }

                    userDao.SendUnregisteredInvitation(potluckId, email); //  send to the invitations table whether they have an account or not
                    output.Add(email); // return list of emails to the inviter
                }

                return Ok(output);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
