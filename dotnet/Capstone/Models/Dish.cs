using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace Capstone.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public string Recipe { get; set; }
        public int CourseId { get; set; }
        public List<int> DishReviews { get; set; } = new List<int>();
        public double Rating
        {
            get
            {
                double averageRating = 0;
                if (!DishReviews.IsNullOrEmpty())
                {
                    foreach (int item in DishReviews)
                    {
                        averageRating += item;
                    }
                    return averageRating / DishReviews.Count;
                }
                return averageRating;
            }
        }
    }
    public class NewDishDTO
    {
        public string Name { get; set; }
        public string Recipe { get; set; }
        public int CourseId { get; set; }
    }
    public class UpdateDishDTO
    {
        public string Name { get; set; }
        public string Recipe { get; set; }
        public int NewReview { get; set; }

    }
}
