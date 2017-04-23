using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.EgeViewModels;
using WebApiTest4.Models.EgeModels;

namespace WebApiTest4.Services.Impls
{
    public class TopicServiceImpl : ITopicService
    {
        private readonly EgeDbContext _context;
        public TopicServiceImpl(EgeDbContext context)
        {
            _context = context;
        }
        public IEnumerable<EgeTopicViewModel> GetTopics()
        {
            return _context.TaskTopics.OrderBy(x => x.Id).ToList().Select(x => new EgeTopicViewModel(x));
        }
    }
}