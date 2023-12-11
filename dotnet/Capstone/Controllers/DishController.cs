using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DishController : ControllerBase
    {
        private readonly IPotluckDao potluckDao;
        private readonly IDishDao dishDao;
        private readonly IUserDao userDao;

        public DishController(IPotluckDao potluckDao, IUserDao userDao, IDishDao dishDao)
        {
            this.userDao = userDao;
            this.dishDao = dishDao;
            this.potluckDao = potluckDao;
        }

        [HttpGet("/dishes/{dishId}")]
        public ActionResult<Dish> GetDishById(int dishId)
        {
            try
            {
                Dish output = dishDao.GetDishById(dishId);
                if (output != null)
                {
                    return Ok(output);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("/users/{userId}/dishes")]
        public ActionResult<IList<Dish>> GetDishesByUserId(int userId)
        {
            try
            {
                IList<Dish> output = null;
                output = dishDao.GetDishesByUserId(userId);
                if (output != null)
                {
                    return Ok(output);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("/potlucks/{potluckId}/dishes")]
        public ActionResult<List<Dish>> GetDishesByPotluckId(int potluckId)
        {
            try
            {
                List<Dish> output = new List<Dish>(dishDao.GetDishesByPotluckId(potluckId));
                if (output != null)
                {
                    return Ok(output);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("/potlucks/{potluckId}/menu")]
        public ActionResult<Dish> CreateNewDish(NewDishDTO newDish, int potluckId)
        {
            try
            {
                Dish addedDish = null;
                if (newDish.Creator.IsNullOrEmpty() || newDish.Name.IsNullOrEmpty() ||
                    newDish.CourseId <= 0)
                {
                    return BadRequest(@"A new dish must have info about who made it, 
                                  the name of the dish, and the course ID number.");
                }

                addedDish = dishDao.CreateNewDish(newDish, newDish.UserId, potluckId);

                return Created($"/potlucks/{potluckId}/menu/{addedDish.DishId}", addedDish);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPut("/dishes/{dishId}/update")]
        public ActionResult<Dish> UpdateDish(UpdateDishDTO editedDish, int dishId)
        {
            try
            {
                User user = userDao.GetUserByUsername(editedDish.Creator);
                Dish dishToUpdate = dishDao.GetDishById(dishId);
                if (user.UserId != editedDish.UserId || dishToUpdate == null)
                {
                    return BadRequest();
                }

                dishToUpdate = dishDao.UpdateDish(editedDish, dishId);
                return Ok(dishToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}