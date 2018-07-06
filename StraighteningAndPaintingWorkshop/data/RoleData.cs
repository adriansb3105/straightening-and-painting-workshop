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
    public class RoleData
    {
        String connectionString;

        public RoleData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Role> GetRoles()
        {
            String sqlSelect = "select role_id, role_description from role_user;";
            SqlDataAdapter daRole = new SqlDataAdapter(sqlSelect, new SqlConnection(connectionString));
            //daUser.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsRoles = new DataSet();
            daRole.Fill(dsRoles, "role_user");

            Dictionary<int, Role> dictionary = new Dictionary<int, Role>();
            Role role = null;
            foreach (DataRow row in dsRoles.Tables["role_user"].Rows)
            {
                int roleId = Int32.Parse(row["role_id"].ToString());
                if (dictionary.ContainsKey(roleId) == false)
                {
                    role = new Role(roleId, row["role_description"].ToString());
                    dictionary.Add(roleId, role);
                }
            }
            return dictionary.Values.ToList<Role>();
        }
    }
}
