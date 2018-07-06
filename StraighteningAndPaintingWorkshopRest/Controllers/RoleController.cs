using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StraighteningAndPaintingWorkshop.data;
using StraighteningAndPaintingWorkshop.domain;

namespace StraighteningAndPaintingWorkshopRest.Controllers
{
    [Produces("application/json")]
    [Route("api/Role")]
    public class RoleController : Controller
    {

        private readonly IConfiguration configuration;
        private RoleData roleData;

        public RoleController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.roleData = new RoleData(configuration.GetConnectionString("DefaultConnection").ToString());
        }

        // GET: api/Role
        [HttpGet]
        public IEnumerable<Role> Get()
        {
            return this.roleData.GetRoles();
        }
        
        // POST: api/Role
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Role/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
