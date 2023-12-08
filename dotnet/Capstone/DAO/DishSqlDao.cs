using Capstone.Exceptions;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class DishSqlDao : IDishDao
    {
        private readonly string connectionString;
        public DishSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public List<Dish> GetDishes()
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

                    while (reader.Read())
                    {
                        Dish dish = MapRowToDish(reader);
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
            Dish dish = null;
            string sql = "SELECT dish_id, dish_name, recipe " +
                "FROM dishes " +
                "WHERE dish_id = @dish_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@dish_id", dishId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        dish = MapRowToDish(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL error occured.", ex);
            }
            return dish;
        }

        public IList<Dish> GetDishesByUserId(int userId)
        {
            IList<Dish> dishes = new List<Dish>();
            // TODO: I think this SQL statement will work, but when I execute it I'm not getting what I expect
            // May need to add UserId as a property of Dish?
            string sql = "SELECT dishes.dish_id, dish_name, recipe " +
                "FROM dishes " +
                "JOIN user_dish AS ud ON ud.dish_id = dishes.dish_id " +
                "JOIN users AS u ON u.user_id = ud.user_id " +
                "WHERE u.user_id = @user_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Dish dish = MapRowToDish(reader);
                        dishes.Add(dish);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL error occured.", ex);
            }
            return dishes;
        }
        public IList<Dish> GetDishesByPotluckId(int potluckId)
        {
            IList<Dish> dishes = new List<Dish>();
            // TODO: Again, I believe this SQL statement will work,
            // but some connection is missing when I try to execute it in the DB
            string sql = "SELECT dishes.dish_id, dish_name, recipe " +
                "FROM dishes " +
                "JOIN potluck_dish AS pd ON pd.dish_id = dishes.dish_id " +
                "JOIN potlucks AS p ON p.potluck_id = pd.potluck_id " +
                "WHERE p.potluck_id = @potluck_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@potluck_id", potluckId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Dish dish = MapRowToDish(reader);
                        dishes.Add(dish);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL error occured.", ex);
            }
            return dishes;
        }
        //public Dish CreateNewDish(NewDishDTO addedDish, int userId, int potluckId)
        //{
        //    //insert into dishes
        //    //insert into dish_user.
        //    //insert into dish_potluck.
        //    Dish newDish = null;
        //    string sql1 = @"INSERT INTO dishes (dish_name, recipe, rating, course_id)
        //                    OUTPUT INSERTED.dish_id
        //                    VALUES (@dish_name, @recipe, @rating, @course_id);";

        //    string sql2 = @"INSERT INTO user_dish (user_id, dish_id)
        //                    VALUES (@ID, @ID)";

        //    int newDishId = 0;

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            SqlCommand cmd = new SqlCommand(sql1, conn);
        //            cmd.Parameters.AddWithValue("@dish_name", addedDish.Name);
        //            cmd.Parameters.AddWithValue("@recipe", addedDish.Recipe);
        //            cmd.Parameters.AddWithValue("@rating", addedDish.Rating);
        //            cmd.Parameters.AddWithValue("@course_id", addedDish.CourseId);
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new DaoException("A SQL error occured.", ex);
        //    }
        //    return newDish;
        //}
        //public Dish UpdateDish(UpdateDishDTO updatedDish, int dishId)
        //{

        //    //UPDATE dishes SET col = value, col2 = value WHERE dish_id = @dishId

        //    Dish modifiedDish = GetDishById(dishId);
        //    if (modifiedDish == null)
        //    {

        //    }
        //    return modifiedDish;
        //}
        private Dish MapRowToDish(SqlDataReader reader)
        {
            Dish dish = new Dish();
            dish.DishId = Convert.ToInt32(reader["dish_id"]);
            dish.Name = Convert.ToString(reader["dish_name"]);
            dish.Recipe = Convert.ToString(reader["recipe"]);
            dish.CourseId = Convert.ToInt32(reader["course_id"]);


            return dish;
        }
    }
}
