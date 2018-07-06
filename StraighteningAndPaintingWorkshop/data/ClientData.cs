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
    public class ClientData
    {
        String connectionString;

        public ClientData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Client> GetClients()
        {
            String sqlSelect = "select client_identification, name, lastname, telephone, address from client;";
            SqlDataAdapter daClient = new SqlDataAdapter(sqlSelect, new SqlConnection(connectionString));
            //daUser.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsClients = new DataSet();
            daClient.Fill(dsClients, "client");

            Dictionary<String, Client> dictionary = new Dictionary<String, Client>();
            Client client = null;
            foreach (DataRow row in dsClients.Tables["client"].Rows)
            {
                String client_identification = row["client_identification"].ToString();
                if (dictionary.ContainsKey(client_identification) == false)
                { 
                    client = new Client(client_identification, row["name"].ToString(), row["lastname"].ToString(), row["telephone"].ToString(), row["address"].ToString());
                    dictionary.Add(client_identification, client);
                }
            }
            return dictionary.Values.ToList<Client>();
        }

        public Client InsertClient(Client client)
        {
            SqlCommand cmdClient = new SqlCommand();
            cmdClient.CommandText = "insert_client";
            cmdClient.CommandType = System.Data.CommandType.StoredProcedure;
            cmdClient.Parameters.Add(new SqlParameter("@client_identification", client.ClientIdentification));
            cmdClient.Parameters.Add(new SqlParameter("@name", client.Name));
            cmdClient.Parameters.Add(new SqlParameter("@lastname", client.Lastname));
            cmdClient.Parameters.Add(new SqlParameter("@telephone", client.Telephone));
            cmdClient.Parameters.Add(new SqlParameter("@address", client.Address));

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                cmdClient.Connection = connection;
                cmdClient.Transaction = transaction;
                cmdClient.ExecuteNonQuery();

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
            return client;
        }

        public Client UpdateClient(string email, Client client)
        {
            SqlCommand cmdClient = new SqlCommand();
            cmdClient.CommandText = "update_client";
            cmdClient.CommandType = System.Data.CommandType.StoredProcedure;
            cmdClient.Parameters.Add(new SqlParameter("@client_identification", email));
            cmdClient.Parameters.Add(new SqlParameter("@name", client.Name));
            cmdClient.Parameters.Add(new SqlParameter("@lastname", client.Lastname));
            cmdClient.Parameters.Add(new SqlParameter("@telephone", client.Telephone));
            cmdClient.Parameters.Add(new SqlParameter("@address", client.Address));

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                cmdClient.Connection = connection;
                cmdClient.Transaction = transaction;
                cmdClient.ExecuteNonQuery();

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

            return client;
        }
    }
}
