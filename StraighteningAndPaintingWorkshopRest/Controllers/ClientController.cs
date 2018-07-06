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
    [Route("api/Client")]
    public class ClientController : Controller
    {
        private readonly IConfiguration configuration;
        private ClientData clientData;

        public ClientController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.clientData = new ClientData(configuration.GetConnectionString("DefaultConnection").ToString());
        }

        // GET: api/Client
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return this.clientData.GetClients();
        }
       
        // POST: api/Client
        [HttpPost]
        public Client Post([FromBody]Client value)
        {
            return this.clientData.InsertClient(value);
        }
        
        // PUT: api/Client/5
        [HttpPut("{email}")]
        public Client Put(String email, [FromBody]Client value)
        {
            return this.clientData.UpdateClient(email, value);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
