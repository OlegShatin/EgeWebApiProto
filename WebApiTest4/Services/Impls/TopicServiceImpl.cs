using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.EgeViewModels;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Services.Impls
{
    public class TopicServiceImpl : ITopicService
    {
        private readonly ExamAppDbContext _context;
        public TopicServiceImpl(ExamAppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<EgeTopicViewModel> GetTopics()
        {
            return _context.TaskTopics.OrderBy(x => x.Id).ToList().Select(x => new EgeTopicViewModel(x));
        }
    }
}