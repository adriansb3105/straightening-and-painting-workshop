﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StraighteningAndPaintingWorkshop.domain
{
    public class WorkDetail
    {
        private int workDetailId;
        private float productsPrice;
        private string description;
        private int workOrderId;
        private List<RequiredProduct> products;

        public WorkDetail(int workDetailId, float productsPrice, string description, int workOrderId, List<RequiredProduct> products)
        {
            this.workDetailId = workDetailId;
            this.productsPrice = productsPrice;
            this.description = description;
            this.workOrderId = workOrderId;
            this.products = products;
        }

        public int WorkDetailId { get => workDetailId; set => workDetailId = value; }
        public float ProductsPrice { get => productsPrice; set => productsPrice = value; }
        public string Description { get => description; set => description = value; }
        public int WorkOrderId { get => workOrderId; set => workOrderId = value; }
        public List<RequiredProduct> Products { get => products; set => products = value; }
    }
}
