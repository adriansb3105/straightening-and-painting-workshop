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
    [Route("api/Vehicle")]
    public class VehicleController : Controller
    {
        private readonly IConfiguration configuration;
        private VehicleData vehicleData;

        public VehicleController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.vehicleData = new VehicleData(configuration.GetConnectionString("DefaultConnection").ToString());
        }

        // GET: api/Vehicle
        [HttpGet]
        public IEnumerable<Vehicle> Get()
        {
            return this.vehicleData.GetVehicles();
        }

        // POST: api/Vehicle
        [HttpPost]
        public Vehicle Post([FromBody]Vehicle value)
        {
            return this.vehicleData.InsertVehicle(value);
        }
        
        // PUT: api/Vehicle/5
        [HttpPut("{licenseNumber}")]
        public Vehicle Put(string licenseNumber, [FromBody]Vehicle value)
        {
            return this.vehicleData.UpdateVehicle(licenseNumber, value);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
