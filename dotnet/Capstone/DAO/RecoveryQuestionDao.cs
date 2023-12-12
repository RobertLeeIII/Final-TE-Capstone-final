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
            string sql = "Select recovery_questions.question_id, question_text from recovery_questions JOIN user_recovery on user_recovery.question_id = recovery_questions.question_id JOIN users on users.user_id = user_recovery.user_id WHERE email = @email;";
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
