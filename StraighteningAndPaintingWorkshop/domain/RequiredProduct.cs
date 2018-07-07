using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StraighteningAndPaintingWorkshop.domain
{
    public class RequiredProduct
    {
        private int requiredProductId;
        private string material;
        private int quantity;
        private float price;
        private int workDetailId;

        public RequiredProduct(int requiredProductId, string material, int quantity, float price, int workDetailId)
        {
            this.requiredProductId = requiredProductId;
            this.material = material;
            this.quantity = quantity;
            this.price = price;
            this.workDetailId = workDetailId;
        }

        public int RequiredProductId { get => requiredProductId; set => requiredProductId = value; }
        public string Material { get => material; set => material = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public float Price { get => price; set => price = value; }
        public int WorkDetailId { get => workDetailId; set => workDetailId = value; }
    }
}
