using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.ApiViewModels
{
    public class ExamTopicViewModel
    {
        public ExamTopicViewModel(TaskTopic sourseTopic)
        {
            id = sourseTopic.Id;
            title = sourseTopic.Name;
        }
        public int id { get; private set; }
        public string title { get; private set; }


    }
}