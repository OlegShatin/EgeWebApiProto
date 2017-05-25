using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using WebApiTest4.Services;
using WebApiTest4.Util;

namespace WebApiTest4.Controllers
{
    [ClaimsAuthorize(ClaimTypes.Role, "teacher")]
    public class SolvedController : ApiController
    {
        private readonly ISolvedTasksService _solvedTasksService;
        public SolvedController(ISolvedTasksService solvedTasksService)
        {
            _solvedTasksService = solvedTasksService;
        }
        [HttpGet]
        public IHttpActionResult GetForMy([FromUri]int offset, [FromUri]int limit)
        {
            if (offset >= 0 && limit >= 0)
            {
                return Ok(_solvedTasksService.GetUncheckedAttemptsOfMyStudents(User.Identity.GetUserId<int>(), offset, limit));
            }
            else
            {
                return BadRequest();
            }
            
        }
        [HttpGet]
        public IHttpActionResult GetByTopic([FromUri]int topic_id, [FromUri]int offset, [FromUri]int limit)
        {

            if (offset >= 0 && limit >= 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpGet]
        public IHttpActionResult GetByType([FromUri]int type, [FromUri]int offset, [FromUri]int? limit)
        {

            if (offset >= 0 && limit >= 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IHttpActionResult GetByStudent([FromUri]int student_id, [FromUri]int offset, [FromUri]int limit)
        {

            if (offset >= 0 && limit >= 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Solved
        public void Post([FromBody]string value)
        {
        }
        
    }
}
