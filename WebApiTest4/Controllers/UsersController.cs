using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Services;

namespace WebApiTest4.Controllers
{
    [Authorize(Roles = "student")]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService service)
        {
            _userService = service;
        }
        //// GET: api/Users
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Users/5
        public UserViewModel Get(int id)
        {
            return _userService.GetUser(id);
        }

        //// POST: api/Users
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Users/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Users/5
        //public void Delete(int id)
        //{
        //}
        
    }
}
