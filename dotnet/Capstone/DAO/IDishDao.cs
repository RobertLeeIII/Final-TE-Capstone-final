using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IDishDao
    {
        public IList<Dish> GetDishes();
        public Dish GetDishById(int dishId);
        public Dish GetDishByUserId(int userId);
        public Dish GetDishByPotluckId(int potluckId);
        public Dish CreateNewDish(NewDishDTO newDish, int userId, int potluckId);
        public Dish UpdateDish(UpdateDishDTO updateDish, int dishId);


    }
}
