using Capstone.Exceptions;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class PotluckSqlDao : IPotluckDao
    {
        private readonly string connectionString;

        public PotluckSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public IList<Potluck> GetPotlucks()
        {
            IList<Potluck> potlucks = new List<Potluck>();
            string sql = "SELECT potluck_id, host_id, potluck_name, summary, location, " +
                "time, theme, is_recurring, repeat_interval, status " +
                "FROM potlucks;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Potluck potluck = MapRowToPotluck(reader);
                        potlucks.Add(potluck);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL exception occured.", ex);
            }
            return potlucks;
        }
        public Potluck GetPotluckById(int potluckId)
        {
            Potluck potluck = null;
            string sql = "SELECT potluck_id, host_id, potluck_name, summary, location, " +
                "time, theme, is_recurring, repeat_interval, status " +
                "FROM potlucks " +
                "WHERE potluck_id = @potluck_id;";

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
                        potluck = MapRowToPotluck(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL exception occured.", ex);
            }
            return potluck;
        }
        public Potluck GetPotluckByName(string potluckName)
        {
            Potluck potluck = null;
            string sql = "SELECT potluck_id, host_id, potluck_name, summary, location, " +
                "time, theme, is_recurring, repeat_interval, status " +
                "FROM potlucks " +
                "WHERE potluck_name = @potluck_name;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@potluck_name", potluckName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        potluck = MapRowToPotluck(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL exception occured.", ex);
            }
            return potluck;
        }
        public IList<Potluck> GetPotlucksByUserId(int userId)
        {
            IList<Potluck> potlucks = new List<Potluck>();
            string sql = "SELECT potlucks.potluck_id, host_id, potluck_name, summary, location, " +
                "time, theme, is_recurring, repeat_interval, potlucks.status " +
                "FROM potlucks " +
                "JOIN potluck_user AS pu ON pu.potluck_id = potlucks.potluck_id " +
                "JOIN users AS u ON u.user_id = pu.user_id " +
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
                        Potluck potluck = MapRowToPotluck(reader);
                        potlucks.Add(potluck);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL error occurred", ex);
            }
            return potlucks;
        }
        public IList<Potluck> getPotluckByHostID(int hostId)
        {
            IList<Potluck> potlucks = new List<Potluck>();
            string sql = "SELECT potluck_id, host_id, potluck_name, summary, location, " +
                "time, theme, is_recurring, repeat_interval " +
                "FROM potlucks " +
                "JOIN users ON user_id = host_id " +
                "WHERE host_id = @host_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@host_id", hostId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Potluck potluck = MapRowToPotluck(reader);
                        potlucks.Add(potluck);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("A SQL error occurred", ex);
            }
            return potlucks;
        }
        public Potluck CreatePotluck(NewPotluckDTO incomingPotluck)
        {
            Potluck newPotluck = null;
            string sql = "INSERT INTO potlucks (host_id, potluck_name, summary, location, time, theme, is_recurring, repeat_interval, status) " +
                "OUTPUT INSERTED.potluck_id " +
                "VALUES (@host_id, @potluck_name, @summary, @location, @time, @theme, @isRecurring, @repeatInterval, @status);";

            int newPotluckId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@host_id", incomingPotluck.HostId);
                    cmd.Parameters.AddWithValue("@potluck_name", incomingPotluck.Name);
                    cmd.Parameters.AddWithValue("@summary", incomingPotluck.Summary);
                    cmd.Parameters.AddWithValue("@location", incomingPotluck.Location);
                    cmd.Parameters.AddWithValue("@time", incomingPotluck.Time);
                    cmd.Parameters.AddWithValue("@theme", incomingPotluck.Theme);
                    cmd.Parameters.AddWithValue("@isRecurring", incomingPotluck.isRecurring);
                    cmd.Parameters.AddWithValue("@repeatInterval", incomingPotluck.RepeatInterval);
                    cmd.Parameters.AddWithValue("@status", incomingPotluck.Status);

                    newPotluckId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                newPotluck = GetPotluckById(newPotluckId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL exception occured.", ex);
            }
            return newPotluck;
        }
        public Potluck UpdatePotluck(UpdatePotluckDTO updatedPotluck, int potluckId)
        {
            Potluck editedPotluck = null;
            string sql = "UPDATE potlucks " +
                "SET potluck_name = @potluck_name, summary = @summary, location = @location, " +
                "time = @time, theme = @theme, is_recurring = @is_recurring, repeat_interval = @repeat_interval " +
                "WHERE potluck_id = @potluck_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@potluck_name", updatedPotluck.Name);
                    cmd.Parameters.AddWithValue("@summary", updatedPotluck.Summary);
                    cmd.Parameters.AddWithValue("@location", updatedPotluck.Location);
                    cmd.Parameters.AddWithValue("@time", updatedPotluck.Time);
                    cmd.Parameters.AddWithValue("@theme", updatedPotluck.Theme);
                    cmd.Parameters.AddWithValue("@is_recurring", updatedPotluck.isRecurring);
                    cmd.Parameters.AddWithValue("@repeat_interval", updatedPotluck.RepeatInterval);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new DaoException("Zero rows affected, but expecting at least one.");
                    }
                    editedPotluck = GetPotluckById(potluckId);
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL exception occured.", ex);
            }
            return editedPotluck;
        }
        public int DeletePotluck(int potluckId)
        {
            int rowsAffected = 0;
            string sql = "DELETE FROM potlucks WHERE potluck_id = @potluck_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@potluck_id", potluckId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL error occured.", ex);
            }
            return rowsAffected;
        }
        public int UninviteUser(int potluckId, int userId)
        {
            int rowsAffected = 0;
            string sql1 = "DELETE FROM potluck_user " +
                "WHERE potluck_user.user_id = @user_id " +
                "AND potluck_user.potluck_id = @potluck_id;";
            // TODO: figure out second SQL statement that lets us remove a dish that the removed user was bringing
            string sql2 = "DELETE FROM potluck_dish WHERE potluck_id = @potluck_id AND dish_id IN (SELECT dish_id from dishes WHERE dish_id IN (select user_id FROM user_dish WHERE user_id = @user_id));";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql1, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@potluck_id", potluckId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL error occured.", ex);
            }
            return rowsAffected;
        }
        private Potluck MapRowToPotluck(SqlDataReader reader)
        {
            Potluck potluck = new Potluck();
            potluck.PotluckId = Convert.ToInt32(reader["potluck_id"]);
            potluck.HostId = Convert.ToInt32(reader["host_id"]);
            potluck.Name = Convert.ToString(reader["potluck_name"]);
            potluck.Summary = Convert.ToString(reader["summary"]);
            potluck.Location = Convert.ToString(reader["location"]);
            potluck.Time = Convert.ToDateTime(reader["time"]);
            potluck.Theme = Convert.ToString(reader["theme"]);
            potluck.isRecurring = Convert.ToBoolean(reader["is_recurring"]);
            potluck.RepeatInterval = Convert.ToInt32(reader["repeat_interval"]);
            potluck.Status = Convert.ToString(reader["status"]);

            return potluck;
        }
    }
}
