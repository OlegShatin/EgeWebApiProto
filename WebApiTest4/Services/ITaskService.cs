using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTest4.EgeViewModels;
using WebApiTest4.EgeViewModels.BindingModels;

namespace WebApiTest4.Services
{
    public interface ITaskService
    {
        EgeTaskViewModel GetTask(int id);
        IEnumerable<EgeTaskViewModel> GetSortedTasks(int? topicId, int offset, int limit);
        dynamic CheckAnswers(string trainType, IEnumerable<TaskAnswerBindingModel> answers, int userId);
        IEnumerable<EgeTaskViewModel> GenerateNewExamTrain(int userId);
    }
}
