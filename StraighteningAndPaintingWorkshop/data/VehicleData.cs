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
    public class VehicleData
    {
        String connectionString;

        public VehicleData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Vehicle> GetVehicles()
        {
            String sqlSelect = "select license_number, color, brand, style, year, capacity, weight, chassis_number from vehicle;";
            SqlDataAdapter daVehicle = new SqlDataAdapter(sqlSelect, new SqlConnection(connectionString));
            //daUser.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsVehicles = new DataSet();
            daVehicle.Fill(dsVehicles, "vehicle");

            Dictionary<String, Vehicle> dictionary = new Dictionary<String, Vehicle>();
            Vehicle vehicle = null;
            foreach (DataRow row in dsVehicles.Tables["vehicle"].Rows)
            {
                String licenseNumber = row["license_number"].ToString();
                if (dictionary.ContainsKey(licenseNumber) == false)
                {                        
                    vehicle = new Vehicle(licenseNumber, row["color"].ToString(), row["brand"].ToString(), row["style"].ToString(), Int32.Parse(row["year"].ToString()), Int32.Parse(row["capacity"].ToString()), float.Parse(row["weight"].ToString()), row["chassis_number"].ToString());
                    dictionary.Add(licenseNumber, vehicle);
                }
            }
            return dictionary.Values.ToList<Vehicle>();
        }

        public Vehicle InsertVehicle(Vehicle vehicle)
        {
            SqlCommand cmdVehicle = new SqlCommand();
            cmdVehicle.CommandText = "insert_vehicle";
            cmdVehicle.CommandType = System.Data.CommandType.StoredProcedure;
            cmdVehicle.Parameters.Add(new SqlParameter("@license_number", vehicle.LicenseNumber));
            cmdVehicle.Parameters.Add(new SqlParameter("@color", vehicle.Color));
            cmdVehicle.Parameters.Add(new SqlParameter("@brand", vehicle.Brand));
            cmdVehicle.Parameters.Add(new SqlParameter("@style", vehicle.Style));
            cmdVehicle.Parameters.Add(new SqlParameter("@year", vehicle.Year));
            cmdVehicle.Parameters.Add(new SqlParameter("@capacity", vehicle.Capacity));
            cmdVehicle.Parameters.Add(new SqlParameter("@weight", vehicle.Weight));
            cmdVehicle.Parameters.Add(new SqlParameter("@chassis_number", vehicle.Chassis_number));
            
            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                cmdVehicle.Connection = connection;
                cmdVehicle.Transaction = transaction;
                cmdVehicle.ExecuteNonQuery();

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
            return vehicle;
        }

        public Vehicle UpdateVehicle(string licenseNumber, Vehicle vehicle)
        {
            SqlCommand cmdVehicle = new SqlCommand();
            cmdVehicle.CommandText = "update_vehicle";
            cmdVehicle.CommandType = System.Data.CommandType.StoredProcedure;
            cmdVehicle.Parameters.Add(new SqlParameter("@license_number", vehicle.LicenseNumber));
            cmdVehicle.Parameters.Add(new SqlParameter("@color", vehicle.Color));
            cmdVehicle.Parameters.Add(new SqlParameter("@brand", vehicle.Brand));
            cmdVehicle.Parameters.Add(new SqlParameter("@style", vehicle.Style));
            cmdVehicle.Parameters.Add(new SqlParameter("@year", vehicle.Year));
            cmdVehicle.Parameters.Add(new SqlParameter("@capacity", vehicle.Capacity));
            cmdVehicle.Parameters.Add(new SqlParameter("@weight", vehicle.Weight));
            cmdVehicle.Parameters.Add(new SqlParameter("@chassis_number", vehicle.Chassis_number));

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                cmdVehicle.Connection = connection;
                cmdVehicle.Transaction = transaction;
                cmdVehicle.ExecuteNonQuery();

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

            return vehicle;
        }
    }
}
