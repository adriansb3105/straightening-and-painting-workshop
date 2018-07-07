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
    public class RequiredProductData
    {
        /*String connectionString;

        public RequiredProductData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<RequiredProduct> GetRequiredProducts(int workDetailId)
        {
            String sqlSelect = "select required_product_id, material, quantity, price, work_detail_id from required_product where work_detail_id='"+workDetailId+"';";
            SqlDataAdapter daRequiredProduct = new SqlDataAdapter(sqlSelect, new SqlConnection(connectionString));
            //daUser.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsRequiredProduct = new DataSet();
            daRequiredProduct.Fill(dsRequiredProduct, "required_product");

            Dictionary<int, RequiredProduct> dictionary = new Dictionary<int, RequiredProduct>();
            RequiredProduct requiredProduct = null;
            foreach (DataRow row in dsRequiredProduct.Tables["required_product"].Rows)
            {
                int requiredProductId = Int32.Parse(row["required_product_id"].ToString());
                if (dictionary.ContainsKey(requiredProductId) == false)
                {
                    requiredProduct = new RequiredProduct(requiredProductId, row["material"].ToString(), Int32.Parse(row["quantity"].ToString()), float.Parse(row["price"].ToString()), Int32.Parse(row["work_detail_id"].ToString()));
                    dictionary.Add(requiredProductId, requiredProduct);
                }
            }
            return dictionary.Values.ToList<RequiredProduct>();
        }

        public RequiredProduct InsertRequiredProduct(RequiredProduct requiredProduct)
        {
            SqlCommand cmdRequiredProduct = new SqlCommand();
            cmdRequiredProduct.CommandText = "insert_required_product";
            cmdRequiredProduct.CommandType = System.Data.CommandType.StoredProcedure;
            cmdRequiredProduct.Parameters.Add(new SqlParameter("@material", requiredProduct.Material));
            cmdRequiredProduct.Parameters.Add(new SqlParameter("@quantity", requiredProduct.Quantity));
            cmdRequiredProduct.Parameters.Add(new SqlParameter("@price", requiredProduct.Price));
            cmdRequiredProduct.Parameters.Add(new SqlParameter("@work_detail_id", requiredProduct.WorkDetailId));

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                cmdRequiredProduct.Connection = connection;
                cmdRequiredProduct.Transaction = transaction;
                requiredProduct.RequiredProductId = Int32.Parse(cmdRequiredProduct.ExecuteScalar().ToString());

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
            return requiredProduct;
        }

        public RequiredProduct UpdateRequiredProduct(int requiredProductId, RequiredProduct requiredProduct)
        {
            SqlCommand cmdRequiredProduct = new SqlCommand();
            cmdRequiredProduct.CommandText = "update_required_product";
            cmdRequiredProduct.CommandType = System.Data.CommandType.StoredProcedure;
            cmdRequiredProduct.Parameters.Add(new SqlParameter("@required_product_id", requiredProductId));
            cmdRequiredProduct.Parameters.Add(new SqlParameter("@material", requiredProduct.Material));
            cmdRequiredProduct.Parameters.Add(new SqlParameter("@quantity", requiredProduct.Quantity));
            cmdRequiredProduct.Parameters.Add(new SqlParameter("@price", requiredProduct.Price));
            cmdRequiredProduct.Parameters.Add(new SqlParameter("@work_detail_id", requiredProduct.WorkDetailId));

            SqlConnection connection = new SqlConnection(connectionString);
            SqlTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                cmdRequiredProduct.Connection = connection;
                cmdRequiredProduct.Transaction = transaction;

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
            return requiredProduct;
        }*/
    }
}
