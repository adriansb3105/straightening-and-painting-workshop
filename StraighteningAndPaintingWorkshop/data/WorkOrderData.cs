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
    public class WorkOrderData
    {
        String connectionString;

        public WorkOrderData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<WorkOrder> GetWorkOrders(string licenseNumber)
        {
            String sqlSelect = "select work_order_id, description, tentative_date, details_price, labor_price, client_identification, license_number from work_order where license_number = '"+licenseNumber+"';";
            SqlDataAdapter daWorkOrder = new SqlDataAdapter(sqlSelect, new SqlConnection(connectionString));
            //daUser.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsWorkOrder = new DataSet();
            daWorkOrder.Fill(dsWorkOrder, "work_order");

            Dictionary<int, WorkOrder> dictionary = new Dictionary<int, WorkOrder>();
            WorkOrder workOrder = null;
            foreach (DataRow row in dsWorkOrder.Tables["work_order"].Rows)
            {
                int workOrderId = Int32.Parse(row["work_order_id"].ToString());
                if (dictionary.ContainsKey(workOrderId) == false)
                {
                    workOrder = new WorkOrder(workOrderId, row["description"].ToString(), DateTime.Parse(row["tentative_date"].ToString()), float.Parse(row["details_price"].ToString()), float.Parse(row["labor_price"].ToString()), row["client_identification"].ToString(), row["license_number"].ToString());
                    dictionary.Add(workOrderId, workOrder);
                }
            }
            return dictionary.Values.ToList<WorkOrder>();
        }

        public WorkOrder InsertWorkOrder(WorkOrder workOrder)
        {           
            SqlCommand cmdWorkOrder = new SqlCommand();
            cmdWorkOrder.CommandText = "insert_work_order";
            cmdWorkOrder.CommandType = System.Data.CommandType.StoredProcedure;
            cmdWorkOrder.Parameters.Add(new SqlParameter("@description", workOrder.Description));
            cmdWorkOrder.Parameters.Add(new SqlParameter("@tentative_date", workOrder.TentativeDate));
            cmdWorkOrder.Parameters.Add(new SqlParameter("@client_identification", workOrder.ClientIdentification));
            cmdWorkOrder.Parameters.Add(new SqlParameter("@license_number", workOrder.LicenseNumber));

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                cmdWorkOrder.Connection = connection;
                cmdWorkOrder.Transaction = transaction;
                workOrder.WorkOrderId = Int32.Parse(cmdWorkOrder.ExecuteScalar().ToString());
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
            return workOrder;
        }

        public WorkOrder UpdateWorkOrder(int workOrderId, WorkOrder workOrder)
        {
            SqlCommand cmdWorkOrder = new SqlCommand();
            cmdWorkOrder.CommandText = "update_work_order";
            cmdWorkOrder.CommandType = System.Data.CommandType.StoredProcedure;
            cmdWorkOrder.Parameters.Add(new SqlParameter("@work_order_id", workOrderId));
            cmdWorkOrder.Parameters.Add(new SqlParameter("@description", workOrder.Description));
            cmdWorkOrder.Parameters.Add(new SqlParameter("@tentative_date", workOrder.TentativeDate));
            cmdWorkOrder.Parameters.Add(new SqlParameter("@details_price", workOrder.DetailsPrice));
            cmdWorkOrder.Parameters.Add(new SqlParameter("@labor_price", workOrder.LaborPrice));
            cmdWorkOrder.Parameters.Add(new SqlParameter("@client_identification", workOrder.ClientIdentification));
            cmdWorkOrder.Parameters.Add(new SqlParameter("@license_number", workOrder.LicenseNumber));

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                cmdWorkOrder.Connection = connection;
                cmdWorkOrder.Transaction = transaction;
                cmdWorkOrder.ExecuteNonQuery();
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
            return workOrder;
        }
    }
}
