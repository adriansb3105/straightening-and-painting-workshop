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
    public class WorkDetailData
    {
        String connectionString;

        public WorkDetailData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<WorkDetail> GetWorkDetails()
        {
            String sqlSelect = "select work_detail_id, products_price, description, work_order_id from work_detail;";
            SqlDataAdapter daWorkDetail = new SqlDataAdapter(sqlSelect, new SqlConnection(connectionString));
            //daUser.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsWorkDetail = new DataSet();
            daWorkDetail.Fill(dsWorkDetail, "work_detail");

            Dictionary<int, WorkDetail> dictionary = new Dictionary<int, WorkDetail>();
            WorkDetail workDetail = null;
            foreach (DataRow row in dsWorkDetail.Tables["work_detail"].Rows)
            {
                int workDetailId = Int32.Parse(row["work_detail_id"].ToString());
                if (dictionary.ContainsKey(workDetailId) == false)
                {
                    workDetail = new WorkDetail(workDetailId, float.Parse(row["products_price"].ToString()), row["description"].ToString(), Int32.Parse(row["work_order_id"].ToString()));
                    dictionary.Add(workDetailId, workDetail);
                }
            }
            return dictionary.Values.ToList<WorkDetail>();
        }

        public WorkDetail InsertWorkDetail(WorkDetail workDetail)
        {
            SqlCommand cmdWorkOrder = new SqlCommand();
            cmdWorkOrder.CommandText = "insert_work_detail";
            cmdWorkOrder.CommandType = System.Data.CommandType.StoredProcedure;
            cmdWorkOrder.Parameters.Add(new SqlParameter("@description", workDetail.Description));
            cmdWorkOrder.Parameters.Add(new SqlParameter("@work_order_id", workDetail.WorkOrderId));

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                cmdWorkOrder.Connection = connection;
                cmdWorkOrder.Transaction = transaction;
                workDetail.WorkDetailId = Int32.Parse(cmdWorkOrder.ExecuteScalar().ToString());
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
            return workDetail;
        }

        public WorkDetail UpdateWorkDetail(int workDetailId, WorkDetail workDetail)
        {
            SqlCommand cmdWorkDetail = new SqlCommand();
            cmdWorkDetail.CommandText = "update_work_detail";
            cmdWorkDetail.CommandType = System.Data.CommandType.StoredProcedure;
            cmdWorkDetail.Parameters.Add(new SqlParameter("@work_detail_id", workDetailId));
            cmdWorkDetail.Parameters.Add(new SqlParameter("@products_price", workDetail.ProductsPrice));
            cmdWorkDetail.Parameters.Add(new SqlParameter("@description", workDetail.Description));
            cmdWorkDetail.Parameters.Add(new SqlParameter("@work_order_id", workDetail.WorkOrderId));

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                cmdWorkDetail.Connection = connection;
                cmdWorkDetail.Transaction = transaction;
                cmdWorkDetail.ExecuteNonQuery();
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
            return workDetail;
        }
    }
}
