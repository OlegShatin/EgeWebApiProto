using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.ApiViewModels;
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

        public IEnumerable<ExamTopicViewModel> GetTopics()
        {
            return _context.TaskTopics.OrderBy(x => x.Id).ToList().Select(x => new ExamTopicViewModel(x, 0));
        }

        public IEnumerable<ExamTopicViewModel> GetTopicsForUser(int userId)
        {
            if (_context.Users.OfRole("student").Any(x => x.Id == userId))
            {
                return
                    _context.TaskTopics.OrderBy(x => x.Id)
                        .ToList()
                        .Select(
                            topic =>
                                new ExamTopicViewModel(topic,
                                    _context.Users.FirstOrDefault(user => user.Id == userId)?
                                        .Trains.OfType<FreeTrain>()?
                                        .SelectMany(
                                            train =>
                                                train.TaskAttempts.Where(
                                                    attempt => attempt.Points > 0 && attempt.ExamTask.Topic.Id == topic.Id))
                                        ?.GroupBy(attempt => attempt.ExamTask.Id)
                                        .Count() ?? 0));
            }
            return GetTopics();
        }
    }
}