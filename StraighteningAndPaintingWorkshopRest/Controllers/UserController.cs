using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StraighteningAndPaintingWorkshop.domain;
using StraighteningAndPaintingWorkshop.data;
using Microsoft.AspNetCore.Cors;

namespace StraighteningAndPaintingWorkshopRest.Controllers
{
    [Produces("application/json")]
    [EnableCors("SiteCorsPolicy")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IConfiguration configuration;
        private UserData userData;

        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.userData = new UserData(configuration.GetConnectionString("DefaultConnection").ToString());
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return this.userData.GetUsers();
        }
        
        // POST: api/User
        [HttpPost]
        public User Post([FromBody]User value)
        {
            //System.Diagnostics.Debug.WriteLine(value);
            return this.userData.InsertUser(value);
        }

        // POST: /api/user/login
        [Route("/api/user/login")]
        public User LogIn([FromBody]User value)
        {
            return this.userData.LogIn(value);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{email}")]
        public void Delete(string email)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
