using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.ApiViewModels
{
    public class ExamTopicViewModel
    {
        public ExamTopicViewModel(TaskTopic sourseTopic)
        {
            id = sourseTopic.Id;
            title = sourseTopic.Name;
            code = sourseTopic.Code;
            type = sourseTopic.IsShort ? 0 : 1;
            max_points = sourseTopic.PointsPerTask;
        }
        public int id { get; private set; }
        public string title { get; private set; }
        public string code { get; private set; }
        public int type { get; private set; }
        public int max_points { get; private set; }

    }
}