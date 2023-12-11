using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IDishDao
    {
        public IList<Dish> GetDishes();
        public Dish GetDishById(int dishId);
        public IList<Dish> GetDishesByUserId(int userId);
        public IList<Dish> GetDishesByPotluckId(int potluckId);
        public Dish CreateNewDish(NewDishDTO newDish, int userId, int potluckId);
        public Dish UpdateDish(UpdateDishDTO updateDish, int dishId);
        public int DeleteDish(int dishId, int potluckId, int userId);


    }
}
