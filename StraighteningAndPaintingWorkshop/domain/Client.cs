using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StraighteningAndPaintingWorkshop.domain
{
    public class Client
    {
        private string clientIdentification;
        private string name;
        private string lastname;
        private string telephone;
        private string address;

        public Client(string clientIdentification, string name, string lastname, string telephone, string address)
        {
            this.clientIdentification = clientIdentification;
            this.name = name;
            this.lastname = lastname;
            this.telephone = telephone;
            this.address = address;
        }

        public string ClientIdentification { get => clientIdentification; set => clientIdentification = value; }
        public string Name { get => name; set => name = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Address { get => address; set => address = value; }
    }
}
