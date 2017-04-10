using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTest4.EgeViewModels;

namespace WebApiTest4.Services
{
    public interface ITaskService
    {
        EgeTaskViewModel GetTask(int id);
        IEnumerable<EgeTaskViewModel> GetSortedTasks(int? topicId, int offset, int limit);
    }
}
