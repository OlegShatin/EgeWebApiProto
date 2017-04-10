using System;
using System.Collections.Generic;
using System.Linq;
using WebApiTest4.EgeViewModels;
using WebApiTest4.Models.EgeModels;

namespace WebApiTest4.Services.Impls
{
    public class TaskServiceImpl : ITaskService
    {
        
        public TaskServiceImpl(EgeDbContext context)
        {
            _dbContext = context;
        }
        private EgeDbContext _dbContext;
        public EgeTaskViewModel GetTask(int id)
        {
            return _dbContext
                .Tasks
                .Where(x => x.Id == id)
                .ToList()
                .Select(x => new EgeTaskViewModel(x))
                .FirstOrDefault();
        }

        public IEnumerable<EgeTaskViewModel> GetSortedTasks(int? topicId, int offset, int limit)
        {
            return _dbContext
                .Tasks
                .Where(x => topicId == null || x.Topic.Id == topicId) //if there is no topic then just first tasks
                .OrderBy(x => x.Id)
                .Skip(offset)
                .Take(limit)
                .ToList()
                .Select(x => new EgeTaskViewModel(x));
                
            return null;
        }
    }
}