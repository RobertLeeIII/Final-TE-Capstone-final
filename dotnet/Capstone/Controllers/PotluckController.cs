using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Capstone.DAO;
using Capstone.Models;


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
                if(output == null)
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

        //[HttpPost("/users/{userId}/potlucks?action=create")]
        //public ActionResult<Potluck> CreateNewPotluck(newPotluckDto newPotluck, int userId)
        //{
        //    Potluck addedPotluck = potluckDao.CreateNewPotluck(newPotluck);
        //    //Check the date, make sure it's after today


        //    return Created($"/users/{addedPotluck.hostId}/transfers/{addedPotluck.potluckId}", addedPotluck);
        //}

        //[HttpPut("/users/{userId}/potlucks/{potluckId}")]
        //public ActionResult<Potluck> UpdatePotluck(PotluckStatusUpdateDto potluckStatusUpdateDto, int userId, int potluckId)
        //{
        //    Potluck updatingPotluck = potluckDao.GetPotluckById(potluckId);
        //    if(updatingPotluck == null)
        //    {
        //        return NotFound();
        //    }
        //    //Flip the potluck status
        //}
    }
}
