using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Services;

namespace WebApiTest4.Controllers
{
    [Authorize]
    public class ThemesController : ApiController
    {
        private readonly ITopicService _topicService;
        public ThemesController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        public IEnumerable<ExamTopicViewModel> Get()
        {
            return _topicService.GetTopics();
        }
    }
}
