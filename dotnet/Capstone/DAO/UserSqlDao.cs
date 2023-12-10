using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using Microsoft.AspNetCore.Http;

namespace Capstone.DAO
{
    public class UserSqlDao : IUserDao
    {
        private readonly string connectionString;

        public UserSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<User> GetUsers()
        {
            IList<User> users = new List<User>();

            string sql = "SELECT user_id, email, username, password_hash, salt, user_role, diet_rest FROM users";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = MapRowToUser(reader);
                        users.Add(user);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return users;
        }

        public IList<User> GetUsersByPotluckId(int potluckId)
        {
            IList<User> output = new List<User>();

            string sql = @"SELECT users.user_id, email, username, password_hash, salt, user_role, diet_rest FROM users 
                           JOIN potluck_user as pu ON users.user_id = pu.user_id 
                           WHERE potluck_id = @potluckId;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@potluckId", potluckId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User holder = MapRowToUser(reader);
                    output.Add(holder);
                }
            }
            return output;
        }

        public User GetUserById(int userId)
        {
            User user = null;

            string sql = "SELECT user_id, email, username, password_hash, salt, user_role, diet_rest FROM users WHERE user_id = @user_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = MapRowToUser(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return user;
        }

        public User GetUserByUsername(string username)
        {
            User user = null;

            string sql = "SELECT user_id, email, username, password_hash, salt, user_role, diet_rest FROM users WHERE username = @username";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = MapRowToUser(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return user;
        }
        public IList<User> getUsersByPotluckID(int potluckId) //TODO Fix sql statement - take out the *
        {
            IList<User> potLuckUsers = new List<User>();

            string sql = "SELECT * FROM users " +
                "potluck.user AS pu ON pu.user_id = users.user.id " +
                "JOIN potlucks on potlucks.id = pu.potluck_id WHERE potlucks.potluck_id = @potluck_id;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@potluck_id", potluckId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        User potLuckUser = MapRowToUser(reader);
                        potLuckUsers.Add(potLuckUser);
                    }

                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("SQL Exception Occurred", ex);
            }
            return potLuckUsers;

        }
        public InviteUser GetUserByEmail(string email)
        {
            InviteUser output = new InviteUser();
            string sql = @"SELECT user_id, email, username, password_hash, salt, user_role, diet_rest FROM users WHERE email = @email;";

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@email", email);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        User holder = MapRowToUser(reader);
                        output.Email = holder.Email;
                        output.UserId = holder.UserId;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return output;
        }
        public User GetHostByPotluckId(int potluckId)
        {
            string sql = @"SELECT user_id, email, username, password_hash, salt, user_role, diet_rest FROM users 
                           JOIN potlucks ON potlucks.host_id = users.user_id WHERE potluck_id = @potluckId;";
            User output = null;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@potluckId", potluckId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        output = MapRowToUser(reader);
                    }
                }
            }
            catch (SqlException ex)
            { 
                throw new DaoException("SQL exception occurred", ex);
            }
                return output;
        }
        public int SendRegisteredInvitation(int potluckId, int userId)
        {
            string sql = @"INSERT INTO potluck_user (potluck_id, user_id, status) 
                           VALUES (@potluckId, @userId, 'invited');";
            int output = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@potluckId", potluckId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    output = cmd.ExecuteNonQuery(); 
                }
                if(output != 1)
                {
                    throw new DaoException("SQL error. Insertion not completed");//What to do if the insert fails
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return output;
        }
        public int SendUnregisteredInvitation(int potluckId, string email)
        {
            string sql = @"INSERT INTO invitations (potluck_id, email) 
                           VALUES (@potluckId, @email);";
            int output = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@potluckId", potluckId);
                    cmd.Parameters.AddWithValue("@email", email);
                    output = cmd.ExecuteNonQuery();
                }
                if (output != 1)
                {
                    throw new DaoException("SQL error. Insertion not completed");
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return output;
        }

        public User CreateUser(string email, string username, string password, string role, bool dietaryRestriction)
        {
            User newUser = null;

            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);

            string sql = "INSERT INTO users (email, username, password_hash, salt, user_role, diet_rest) " +
                         "OUTPUT INSERTED.user_id " +
                         "VALUES (@email, @username, @password_hash, @salt, @user_role, @diet_rest)";

            int newUserId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@user_role", role);
                    cmd.Parameters.AddWithValue("@diet_rest", dietaryRestriction);

                    newUserId = Convert.ToInt32(cmd.ExecuteScalar());

                }
                newUser = GetUserById(newUserId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return newUser;
        }

        private User MapRowToUser(SqlDataReader reader)
        {
            User user = new User();
            user.UserId = Convert.ToInt32(reader["user_id"]);
            user.Email = Convert.ToString(reader["email"]);
            user.Username = Convert.ToString(reader["username"]);
            user.PasswordHash = Convert.ToString(reader["password_hash"]);
            user.Salt = Convert.ToString(reader["salt"]);
            user.Role = Convert.ToString(reader["user_role"]);
            user.DietaryRestriction = Convert.ToBoolean(reader["diet_rest"]);
            return user;
        }
    }

}


