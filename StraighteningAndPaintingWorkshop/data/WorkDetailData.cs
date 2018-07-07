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

        public List<WorkDetail> GetWorkDetails(int workOrderId)
        {
            String sqlSelect = "select work_detail_id, products_price, description, work_order_id from work_detail where work_order_id='"+workOrderId+"';";
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
                    List<RequiredProduct> products = new List<RequiredProduct>();
                    String sqlProduct = "select required_product_id, material, quantity, price, work_detail_id from required_product where work_detail_id='" + workDetailId + "';";

                    SqlDataAdapter daRequiredProduct = new SqlDataAdapter(sqlSelect, new SqlConnection(connectionString));
                    DataSet dsRequiredProduct = new DataSet();
                    daRequiredProduct.Fill(dsRequiredProduct, "required_product");
                    RequiredProduct requiredProduct = null;

                    foreach (DataRow rowProduct in dsRequiredProduct.Tables["required_product"].Rows)
                    {
                        int requiredProductId = Int32.Parse(rowProduct["required_product_id"].ToString());
                        requiredProduct = new RequiredProduct(requiredProductId, rowProduct["material"].ToString(), Int32.Parse(rowProduct["quantity"].ToString()), float.Parse(rowProduct["price"].ToString()), Int32.Parse(rowProduct["work_detail_id"].ToString()));
                            products.Add(requiredProduct);
                    }

                    workDetail = new WorkDetail(workDetailId, float.Parse(row["products_price"].ToString()), row["description"].ToString(), Int32.Parse(row["work_order_id"].ToString()), products);
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

            SqlCommand cmdRequiredProduct = new SqlCommand();
            cmdRequiredProduct.CommandText = "insert_required_product";
            cmdRequiredProduct.CommandType = System.Data.CommandType.StoredProcedure;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                cmdWorkOrder.Connection = connection;
                cmdWorkOrder.Transaction = transaction;
                cmdRequiredProduct.Connection = connection;
                cmdRequiredProduct.Transaction = transaction;

                int wordDetailId = Int32.Parse(cmdWorkOrder.ExecuteScalar().ToString());
                workDetail.WorkDetailId = wordDetailId;

                List<RequiredProduct> products = workDetail.Products;

                foreach (RequiredProduct requiredProduct in products)
                {
                    cmdRequiredProduct.Parameters.Add(new SqlParameter("@material", requiredProduct.Material));
                    cmdRequiredProduct.Parameters.Add(new SqlParameter("@quantity", requiredProduct.Quantity));
                    cmdRequiredProduct.Parameters.Add(new SqlParameter("@price", requiredProduct.Price));
                    cmdRequiredProduct.Parameters.Add(new SqlParameter("@work_detail_id", wordDetailId));

                    cmdRequiredProduct.ExecuteNonQuery();
                    cmdRequiredProduct.Parameters.Clear();
                }

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
