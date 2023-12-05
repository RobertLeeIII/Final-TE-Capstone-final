using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;


namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PotluckController : ControllerBase
    {
        private readonly IPotluckDao potluckDao;
        private readonly IDishDao dishDao;
        private readonly IUserDao userDao;

        public PotluckController(IPotluckDao potluckDao, IUserDao userDao, IDishDao dishDao)
        {
            this.userDao = userDao;
            this.dishDao = dishDao;
            this.potluckDao = potluckDao;
        }

        [HttpGet("/potlucks/{id}")]  //Endpoint might change
        public ActionResult<Potluck> GetPotluckById(int potluckId)
        {
            try
            {
                Potluck output = potluckDao.GetPotluckById(potluckId);
                if (output == null)
                {
                    return NotFound();
                }
                return output;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("/users/{userId}/potlucks?action=create")]
        public ActionResult<Potluck> CreateNewPotluck(NewPotluckDTO newPotluck, int userId)
        {
            Potluck addedPotluck = potluckDao.CreatePotluck(newPotluck);
            //Check the date, make sure it's after today


            return Created($"/users/{addedPotluck.HostId}/potlucks/{addedPotluck.PotluckId}", addedPotluck);
        }

        [HttpPut("/users/{userId}/potlucks/{potluckId}")]
        public ActionResult<Potluck> UpdatePotluck(UpdatePotluckDTO editedPotluck, int userId, int potluckId)
        {
            try
            {
                Potluck updatingPotluck = potluckDao.GetPotluckById(potluckId);
                if (updatingPotluck == null)
                {
                    return NotFound();
                }
                if (updatingPotluck.HostId != userId)
                {
                    return Unauthorized();
                }

                updatingPotluck = potluckDao.UpdatePotluck(editedPotluck, potluckId);
                return Ok(updatingPotluck);
                //Flip the potluck status
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
    }
}
