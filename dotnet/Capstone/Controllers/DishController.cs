using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
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

        [HttpGet("")]  //BLAAAHHH ENDPOINTS
        public ActionResult<List<Dish>> GetDishes(){
            try{
            IList<Dish> output = new List<Dish>();
            output = dishDao.GetDishes();
            return Ok(output);
        }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("")]  //NEED THE ENDPOINT
        public ActionResult<Dish> GetDishById(int dishId){
            try{
                Dish output = dishDao.GetDishById(dishId);
                if(output == null){
                    return NotFound();
                }
                return output;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("")]  //NEED THE ENDPOINT
        public ActionResult<List<Dish>> GetDishesByUserId(int userId){
            try{
                List<Dish> output = new List<Dish>(dishDao.GetDishesByUserId(userId));
                if (output == null){
                    return NotFound();
                }
                return output;
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("")]  //NEED THE ENDPOINT
        public ActionResult<List<Dish>> GetDishesByPotluckId(int potluckId){
            try{
                List<Dish> output = new List<Dish>(dishDao.GetDishesByPotluckId(potluckId));
                if (output == null){
                    return NotFound();
                }
                return output;
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

         [HttpPost("/users/{userId}/dishes")]  //NEED ENDPOINT
        public ActionResult<Dish> CreateNewDish(NewDishDTO newDish, int userId, int potluckId)
        {
            Dish addedDish = dishDao.CreateNewDish(newDish, userId, potluckId);
        

            return Created($"/users/{userId}/dishes/{addedDish.DishId}", addedDish);
        }

        [HttpPut("/users/{userId}/dishes/{dishId}")]  //ENDPOINT ENDPOINT ENDPOINT BLAH
        public ActionResult<Dish> UpdateDish(UpdateDishDTO editedDish, int userId, int dishId){
            try
            {
                Dish dishToUpdate = dishDao.GetDishById(dishId);
                if (dishToUpdate == null)
                {
                    return NotFound();
                }
                dishToUpdate = dishDao.UpdateDish(editedDish, dishId);
                return Ok(dishToUpdate);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

 










    }}