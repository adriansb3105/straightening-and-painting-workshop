using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StraighteningAndPaintingWorkshop.domain
{
    public class Role
    {
        private int roleId;
        private string description;

        public Role(int roleId, string description)
        {
            this.roleId = roleId;
            this.description = description;
        }

        public int RoleId { get => roleId; set => roleId = value; }
        public string Description { get => description; set => description = value; }
    }
}
