﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StraighteningAndPaintingWorkshop.data;
using StraighteningAndPaintingWorkshop.domain;

namespace StraighteningAndPaintingWorkshopRest.Controllers
{
    [Produces("application/json")]
    [EnableCors("SiteCorsPolicy")]
    [Route("api/WorkOrder")]
    public class WorkOrderController : Controller
    {
        private readonly IConfiguration configuration;
        private WorkOrderData workOrderData;

        public WorkOrderController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.workOrderData = new WorkOrderData(configuration.GetConnectionString("DefaultConnection").ToString());
        }

        // GET: api/WorkOrder/licenseNumber
        [HttpGet("{licenseNumber}")]
        public IEnumerable<WorkOrder> Get(string licenseNumber)
        {
            return this.workOrderData.GetWorkOrders(licenseNumber);
        }
        
        // POST: api/WorkOrder
        [HttpPost]
        public WorkOrder Post([FromBody]WorkOrder value)
        {
            return this.workOrderData.InsertWorkOrder(value);
        }
        
        // PUT: api/WorkOrder/5
        [HttpPut("{id}")]
        public WorkOrder Put(int id, [FromBody]WorkOrder value)
        {
            return this.workOrderData.UpdateWorkOrder(id, value);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
