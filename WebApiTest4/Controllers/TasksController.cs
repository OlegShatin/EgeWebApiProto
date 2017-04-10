using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApiTest4.EgeViewModels;
using WebApiTest4.Services;

namespace WebApiTest4.Controllers
{
    [Authorize]
    public class TasksController : ApiController
    {
        
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        const int defaultLimit = 20;
        //GET: api/Tasks?
        public IEnumerable<EgeTaskViewModel> Get([FromUri]int? topic_id, [FromUri]int? offset, [FromUri]int? limit)
        {
            
            return _taskService.GetSortedTasks(topic_id, offset ?? 0, limit ?? defaultLimit);
        }

        // GET: api/Tasks/5
        public EgeTaskViewModel Get(int id)
        {
            return _taskService.GetTask(id);
        }

        //// POST: api/Tasks
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Tasks/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Tasks/5
        //public void Delete(int id)
        //{
        //}
    }
}
