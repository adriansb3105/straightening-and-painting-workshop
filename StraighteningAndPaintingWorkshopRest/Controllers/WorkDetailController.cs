using System;
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
    [Route("api/WorkDetail")]
    public class WorkDetailController : Controller
    {
        private readonly IConfiguration configuration;
        private WorkDetailData workDetailData;

        public WorkDetailController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.workDetailData = new WorkDetailData(configuration.GetConnectionString("DefaultConnection").ToString());
        }

        // GET: api/WorkDetail
        [HttpGet("{workOrderId}")]
        public IEnumerable<WorkDetail> Get(int workOrderId)
        {
            return this.workDetailData.GetWorkDetails(workOrderId);
        }
        
        // POST: api/WorkDetail
        [HttpPost]
        public WorkDetail Post([FromBody]WorkDetail value)
        {
            return this.workDetailData.InsertWorkDetail(value);
        }
        
        // PUT: api/WorkDetail/5
        [HttpPut("{id}")]
        public WorkDetail Put(int id, [FromBody]WorkDetail value)
        {
            return this.workDetailData.UpdateWorkDetail(id, value);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
