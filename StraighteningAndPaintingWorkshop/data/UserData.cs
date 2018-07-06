using StraighteningAndPaintingWorkshop.domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StraighteningAndPaintingWorkshop.data
{
    public class UserData
    {
        String connectionString;

        public UserData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<User> GetUsers()
        {
            String sqlSelect = "select email, complete_name, role_id from app_user;";
            SqlDataAdapter daUser = new SqlDataAdapter(sqlSelect, new SqlConnection(connectionString));
            //daUser.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsUsers = new DataSet();
            daUser.Fill(dsUsers, "app_user");

            Dictionary<String, User> dictionary = new Dictionary<String, User>();
            User user = null;
            foreach (DataRow row in dsUsers.Tables["app_user"].Rows)
            {
                String email = row["email"].ToString();
                if (dictionary.ContainsKey(email) == false)
                {
                    user = new User(email, row["complete_name"].ToString(), Int32.Parse(row["role_id"].ToString()));
                    dictionary.Add(email, user);
                }
            }
            return dictionary.Values.ToList<User>();
        }

        public User InsertUser(User user)
        {
            SqlCommand cmdUser = new SqlCommand();
            cmdUser.CommandText = "insert_app_user";
            cmdUser.CommandType = System.Data.CommandType.StoredProcedure;
            cmdUser.Parameters.Add(new SqlParameter("@email", user.Email));
            cmdUser.Parameters.Add(new SqlParameter("@pass", user.Password));
            cmdUser.Parameters.Add(new SqlParameter("@complete_name", user.CompleteName));
            cmdUser.Parameters.Add(new SqlParameter("@role_id", user.RoleId));

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                cmdUser.Connection = connection;
                cmdUser.Transaction = transaction;
                cmdUser.ExecuteNonQuery();
                
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                if (transaction != null)
                    transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return user;
        }

        public User LogIn(User user_)
        {

            String sqlSelect = "select email, complete_name, role_id  from app_user where email = '"+user_.Email+"' AND pass = '"+ user_.Password + "';";
            SqlDataAdapter daUser = new SqlDataAdapter(sqlSelect, new SqlConnection(connectionString));
            //daUser.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsUsers = new DataSet();
            daUser.Fill(dsUsers, "app_user");
            
            User user = null;
            foreach (DataRow row in dsUsers.Tables["app_user"].Rows)
            {
                string email_ = row["email"].ToString();
                string complete_name_ = row["complete_name"].ToString();
                int role_id_ = Int32.Parse(row["role_id"].ToString());

                user = new User(email_, complete_name_, role_id_);
            }
            return user;
        }
    }
}
