using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController: ControllerBase
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
        public ActionResult<IList<User>> GetUsersByPotluckID(int potluckId)
        {
            try
            {
                IList<User> output = userDao.GetUsersByPotluckId(potluckId);
                return Ok(output);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
