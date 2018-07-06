using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StraighteningAndPaintingWorkshop.domain
{
    public class WorkOrder
    {
        private int workOrderId;
        private string description;
        private DateTime tentativeDate;
        private float detailsPrice;
        private float laborPrice;
        private string clientIdentification;
        private string licenseNumber;

        public WorkOrder(int workOrderId, string description, DateTime tentativeDate, float detailsPrice, float laborPrice, string clientIdentification, string licenseNumber)
        {
            this.workOrderId = workOrderId;
            this.Description = description;
            this.TentativeDate = tentativeDate;
            this.DetailsPrice = detailsPrice;
            this.LaborPrice = laborPrice;
            this.ClientIdentification = clientIdentification;
            this.LicenseNumber = licenseNumber;
        }

        public int WorkOrderId { get => workOrderId; set => workOrderId = value; }
        public string Description { get => description; set => description = value; }
        public DateTime TentativeDate { get => tentativeDate; set => tentativeDate = value; }
        public float DetailsPrice { get => detailsPrice; set => detailsPrice = value; }
        public float LaborPrice { get => laborPrice; set => laborPrice = value; }
        public string ClientIdentification { get => clientIdentification; set => clientIdentification = value; }
        public string LicenseNumber { get => licenseNumber; set => licenseNumber = value; }
    }
}
