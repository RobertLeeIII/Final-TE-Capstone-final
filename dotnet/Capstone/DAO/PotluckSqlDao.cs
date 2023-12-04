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
            string sql = "SELECT potluck_id, host_id, potluck_name, summary, location, time, theme, is_recurring, repeat_interval" +
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
            string sql = "SELECT potluck_id, host_id, potluck_name, summary, location, time, theme, is_recurring, repeat_interval " +
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
        public Potluck GetPotluckByName(int potluckName)
        {
            Potluck potluck = null;
            string sql = "SELECT potluck_id, host_id, potluck_name, summary, location, time, theme, is_recurring, repeat_interval " +
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
        public Potluck CreatePotluck(NewPotluckDTO incomingPotluck)
        {
            Potluck newPotluck = null;
            string sql = "INSERT INTO potlucks (host_id, potluck_name, summary, location, time, theme, is_recurring, repeat_interval) " +
                "OUTPUT INSERTED.potluck_id " +
                "VALUES (@host_id, @potluck_name, @summary, @location, @time, @theme, @is_reccuring, @repeat_interval);";

            int newPotluckId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    // TODO: Gonna need more parameterized queries. Need to consult about NewPotluckDTO model
                    cmd.Parameters.AddWithValue("@host_id", incomingPotluck.HostId);
                    cmd.Parameters.AddWithValue("@potluck_name", incomingPotluck.Name);
                    cmd.Parameters.AddWithValue("@summary", incomingPotluck.Summary);
                    cmd.Parameters.AddWithValue("@time", incomingPotluck.Time);

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
        // TODO: Unsure of about this one, but I tried...
        public Potluck UpdatePotluck(Potluck potluck, int hostId)
        {
            Potluck updatedPotluck = null;
            // I want to be able to update more than just the time.
            string sql = "UPDATE potlucks " +
                "SET time = @time " +
                "WHERE potluck_id = @potluck_id " +
                "AND host_id = @host_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    // Revist these depending on the SET portion of the SQL statement
                    cmd.Parameters.AddWithValue("@potluck_id", potluck.PotluckId);
                    cmd.Parameters.AddWithValue("@host_id", potluck.HostId);
                    cmd.Parameters.AddWithValue("@time", potluck.Time);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new DaoException("Zero rows affected, but expecting at least one.");
                    }
                    updatedPotluck = GetPotluckById(potluck.PotluckId);
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("A SQL exception occured.", ex);
            }
            return updatedPotluck;
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

            return potluck;
        }
    }
}
