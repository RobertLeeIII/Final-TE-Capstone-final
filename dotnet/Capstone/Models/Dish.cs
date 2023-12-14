using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        //This points to the userId
        [Required]
        public string Creator { get; set; }
        [Required]
        public string Name { get; set; }
        public string Recipe { get; set; }
        public int CourseId { get; set; }
        public List<string> Diets { get; set; }
        public List<string> Allergens { get; set; }
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
        public int UserId { get; set; }
        [Required]
        public string Creator { get; set; }
        [Required]
        public string Name { get; set; }
        public string Recipe { get; set; }
        public int CourseId { get; set; }
        public List<string> Diets { get; set; }
        public List<string> Allergens { get; set; }
    }
    public class UpdateDishDTO
    {
        public int UserId { get; set; }
        [Required]
        public string Creator { get; set; } // This is the username
        [Required]
        public string Name { get; set; }
        public string Recipe { get; set; }
        public int NewReview { get; set; }

    }
}
