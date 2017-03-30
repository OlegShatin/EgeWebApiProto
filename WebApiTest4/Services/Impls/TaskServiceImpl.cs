using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}