using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using Microsoft.AspNetCore.Http;

namespace Capstone.DAO
{
    public class RecoveryQuestionDao : IRecoveryQuestionDao
    {
        private readonly string connectionString;
        public RecoveryQuestionDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public string GetQuestionText(int questionId)
        {
            string sql = "SELECT * FROM recovery_questions where question_id = @question_id;";
            string questionText = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        questionText = Convert.ToString(reader["question_text"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return questionText;
        }
    }
}
