using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Services;

namespace WebApiTest4.Controllers
{
    [Authorize]
    public class TeachersController : ApiController
    {
        private ApplicationUserManager _userManager;
        private readonly IUserService _userService;

        public TeachersController(IUserService service)
        {
            _userService = service;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: api/Teachers
        public IEnumerable<TeacherViewModel> Get()
        {
            return _userService.GetTeachers();
        }

        // GET: api/Teachers/5
        public IHttpActionResult Get(int id)
        {
            if (UserManager.IsInClaimRole(id, "teacher"))
            {
                return Ok(_userService.GetTeacherVMById(id));
            }
            else
            {
                return NotFound();
            }
        }
        
    }
}
