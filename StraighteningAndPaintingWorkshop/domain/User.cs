using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StraighteningAndPaintingWorkshop.domain
{
    public class User
    {
        private string email;
        private string completeName;
        private int roleId;
        private string password;

        public User(string email, string completeName, int roleId)
        {
            this.email = email;
            this.password = "";
            this.completeName = completeName;
            this.roleId = roleId;
        }

        public string Email { get => email; set => email = value; }
        public string CompleteName { get => completeName; set => completeName = value; }
        public int RoleId { get => roleId; set => roleId = value; }
        public string Password { get => password; set => password = value; }
    }
}
