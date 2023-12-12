using Capstone.Exceptions;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Capstone.DAO
{
    public class DishSqlDao : IDishDao
    {
        private readonly string connectionString;
        public DishSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public IList<Dish> GetDishes()
        {
            List<Dish> dishes = new List<Dish>();
            string sql = "SELECT dish_id, dish_name, recipe FROM dishes;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Dish dish = new Dish();
                    while (reader.Read())
                    {
                        dish = MapRowToDish(reader, dish);
                        dishes.Add(dish);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL exception occured.", ex);
            }
            return dishes;
        }
        public Dish GetDishById(int dishId)
        {
            Dish dish = new Dish();
            dish.Allergens = new List<string>();
            dish.Diets = new List<string>();
            string sql = @"SELECT dishes.dish_id, creator, dish_name, recipe, rating, course_id, allergen_name, diet_name
                           FROM dishes 
                           FULL JOIN dish_diet AS dd ON dd.dish_id = dishes.dish_id
                           FULL JOIN dish_allergies AS da ON da.dish_id = dishes.dish_id
                           WHERE dishes.dish_id = @dish_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@dish_id", dishId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        dish = MapRowToDish(reader, dish);
                        if (string.IsNullOrEmpty(dish.Diets[0]))
                        {
                            dish.Diets = new List<string>();
                        }
                        if (string.IsNullOrEmpty(dish.Allergens[0]))
                        {
                            dish.Allergens = new List<string>();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL error occured.", ex);
            }
            dish.Allergens = dish.Allergens.Distinct().ToList();
            dish.Diets = dish.Diets.Distinct().ToList();
            return dish;
        }
        public IList<Dish> GetDishesByUserId(int userId)
        {
            IList<Dish> dishes = new List<Dish>();
            string sql = @"SELECT dishes.dish_id, creator, dish_name, recipe, rating, course_id, allergen_name, diet_name
                           FROM dishes
                           JOIN users AS u ON u.user_id = dishes.creator
                           JOIN dish_diet AS dd ON dd.dish_id = dishes.dish_id
                           JOIN dish_allergies AS da ON da.dish_id = dishes.dish_id
                           WHERE creator = (SELECT TOP 1 username from users WHERE user_id = @user_id)
                           ORDER BY dishes.dish_id ASC;";


            string sql5 = @"SELECT dishes.dish_id, creator, dish_name, recipe, rating, course_id, allergen_name  
                            FROM dishes 
                            JOIN dish_allergies AS da ON da.dish_id = dishes.dish_id 
                            JOIN potluck_dish AS pd ON pd.dish_id = dishes.dish_id 
                            JOIN potlucks AS p ON p.potluck_id = pd.potluck_id 
                            WHERE dishes.dish_id = @dish_id 
                            ORDER BY dishes.dish_id;";

            string sql6 = @"SELECT dishes.dish_id, creator, dish_name, recipe, rating, course_id, diet_name 
                            FROM dishes 
                            JOIN dish_diet AS dd ON dd.dish_id = dishes.dish_id 
                            JOIN potluck_dish AS pd ON pd.dish_id = dishes.dish_id 
                            JOIN potlucks AS p ON p.potluck_id = pd.potluck_id 
                            WHERE dishes.dish_id = @dish_id 
                            ORDER BY dishes.dish_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    int currentId = -1;
                    Dish dish = new Dish();
                    while (reader.Read())
                    {
                        while (currentId == -1)
                        {
                            currentId = Convert.ToInt32(reader["dish_id"]);
                        }

                        if (currentId == Convert.ToInt32(reader["dish_id"]))
                        {
                            dish = MapRowToDish(reader, dish);
                        }
                        else
                        {
                            dishes.Add(dish);
                            currentId = Convert.ToInt32(reader["dish_id"]);
                            dish = new Dish();
                            dish = MapRowToDish(reader, dish);
                        }
                    }
                    dishes.Add(dish);
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL error occured.", ex);
            }
            foreach(Dish dish in dishes)
            {
                dish.Allergens = dish.Allergens.Distinct().ToList();
                dish.Diets = dish.Diets.Distinct().ToList();
            }
            return dishes;
        }
        public IList<Dish> GetDishesByPotluckId(int potluckId)
        {
            IList<Dish> dishes = new List<Dish>();
            string sql = @"SELECT dishes.dish_id, creator, dish_name, recipe, rating, course_id, allergen_name, diet_name
                           FROM dishes
                           JOIN dish_allergies AS da ON da.dish_id = dishes.dish_id
                           JOIN dish_diet AS dd ON dd.dish_id = dishes.dish_id
                           JOIN potluck_dish AS pd ON pd.dish_id = dishes.dish_id
                           JOIN potlucks AS p ON p.potluck_id = pd.potluck_id
                           WHERE p.potluck_id = @potluck_id
                           ORDER BY dishes.dish_id; ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@potluck_id", potluckId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    int currentId = -1;
                    Dish dish = new Dish();
                    while (reader.Read())
                    {

                        while (currentId == -1)
                        {
                            currentId = Convert.ToInt32(reader["potluck_id"]);
                        }

                        if (currentId == Convert.ToInt32(reader["potluck_id"]))
                        {
                            dish = MapRowToDish(reader, dish);
                        }
                        else
                        {
                            dishes.Add(dish);
                            currentId = Convert.ToInt32(reader["potluck_id"]);
                            dish = new Dish();
                            dish = MapRowToDish(reader, dish);
                        }
                    }
                    dishes.Add(dish);
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL error occured.", ex);
            }
            return dishes;
        }
        public Dish CreateNewDish(NewDishDTO newDish, int userId, int potluckId)
        {
            Dish addedDish = null;
            // First insert in SQL2 is effectively the creator of the dish
            string sql1 = @"INSERT INTO dishes (creator, dish_name, recipe, course_id) 
                            OUTPUT INSERTED.dish_id 
                            VALUES (@creator, @dish_name, @recipe, @course_id);";

            // Second one is which potluck the dish is going to
            string sql2 = @"INSERT INTO user_dish (user_id, dish_id) 
                            VALUES (@userID, @dishID);
                            
                            INSERT INTO potluck_dish (potluck_id, dish_id) 
                            VALUES (@potluckID, @dishID);";
            // Third one for dish diet and Allergens
            string sql3 = @"INSERT INTO dish_diet (dish_id, diet_name)
                            VALUES (@dishId, @diet_name);";

            string sql4 = @"INSERT INTO dish_allergies (dish_id, allergen_name)
                            VALUES (@dish_id, @allergen_name);";

            int newDishId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql1, conn);
                    cmd.Parameters.AddWithValue("@creator", newDish.Creator);
                    cmd.Parameters.AddWithValue("@dish_name", newDish.Name);
                    cmd.Parameters.AddWithValue("@recipe", newDish.Recipe);
                    cmd.Parameters.AddWithValue("@course_id", newDish.CourseId);

                    newDishId = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand(sql2, conn);
                    cmd.Parameters.AddWithValue("@userID", userId);
                    cmd.Parameters.AddWithValue("@dishID", newDishId);
                    cmd.Parameters.AddWithValue("@potluckID", potluckId);

                    int counter = 0;
                    counter = cmd.ExecuteNonQuery();
                    counter = 0;

                    for (int i = 0; i < newDish.Diets.Count; i++)
                    {
                        cmd = new SqlCommand(sql3, conn);
                        cmd.Parameters.AddWithValue("@dishID", newDishId);
                        cmd.Parameters.AddWithValue("@diet_name", newDish.Diets[i]);
                        cmd.ExecuteNonQuery();
                        counter++;
                    }
                    counter = 0;
                    for (int i = 0; i < newDish.Allergens.Count; i++)
                    {
                        cmd = new SqlCommand(sql4, conn);
                        cmd.Parameters.AddWithValue("@dish_id", newDishId);
                        cmd.Parameters.AddWithValue("@allergen_name", newDish.Allergens[i]);
                        cmd.ExecuteNonQuery();
                        counter++;
                    }

                }
                addedDish = GetDishById(newDishId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL error occured.", ex);
            }
            addedDish.Allergens = addedDish.Allergens.Distinct().ToList();
            addedDish.Diets = addedDish.Diets.Distinct().ToList();
            return addedDish;
        }
        public Dish UpdateDish(UpdateDishDTO updatedDish, int dishId)
        {
            Dish modifiedDish = GetDishById(dishId);
            //UPDATE dishes SET col = value, col2 = value WHERE dish_id = @dishId
            string sql = @"UPDATE dishes 
                           SET dish_name = @dish_name, recipe = @recipe 
                           WHERE dish_id = @dish_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@dish_name", updatedDish.Name);
                    cmd.Parameters.AddWithValue("@recipe", updatedDish.Recipe);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new DaoException("Zero rows affected. Expecting at least one.");
                    }
                    modifiedDish = GetDishById(dishId);
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL error occured.", ex);
            }
            return modifiedDish;
        }
        public int DeleteDish(int dishId, int potluckId, int userId)
        {
            int rowsAffected = 0;
            string sql = @"DELETE FROM user_dish WHERE user_id = @user_id AND dish_id = @dish_id; 
                           DELETE FROM potluck_dish WHERE potluck_id = @potluck_id AND dish_id = @dish_id; 
                           DELETE FROM dishes WHERE dish_id = @dish_id;";
            // Add these back as needed
            //DELETE FROM dish_diet WHERE dish_id = @dish_id AND diet_id = @diet_id; 
            //DELETE FROM dish_rating WHERE dish_id = @dish_id AND rater = @user_id;
            //DELETE FROM ingredient_dish WHERE ingredient_id = @ingredient_id AND dish_id = @dish_id; 

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@dish_id", dishId);
                    cmd.Parameters.AddWithValue("@potluck_id", potluckId);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL error occured.", ex);
            }
            return rowsAffected;
        }

        //TODO: FIX MAPROWTODISH so it handles the allergens and diets lists
        private Dish MapRowToDish(SqlDataReader reader, Dish input)
        {
            Dish dish = new Dish();
            dish.Allergens = input.Allergens;
            dish.Diets = input.Diets;
            dish.DishId = Convert.ToInt32(reader["dish_id"]);
            dish.Creator = Convert.ToString(reader["creator"]);
            dish.Name = Convert.ToString(reader["dish_name"]);
            dish.Recipe = Convert.ToString(reader["recipe"]);
            dish.CourseId = Convert.ToInt32(reader["course_id"]);
            dish.Allergens.Add(Convert.ToString(reader["allergen_name"]));
            dish.Diets.Add(Convert.ToString(reader["diet_name"]));

            return dish;
        }
    }
}
