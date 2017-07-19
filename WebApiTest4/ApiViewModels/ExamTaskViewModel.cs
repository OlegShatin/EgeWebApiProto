using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.ApiViewModels
{
    public class ExamTaskViewModel
    {
        public ExamTaskViewModel(ExamTask sourseTask, int? user_points = null)
        {
            id = sourseTask.Id;
            text = sourseTask.Text;
            topic_id = sourseTask.Topic.Id;
            type = sourseTask.Topic.IsShort ? 0 : 1;
            code = sourseTask.Topic.Code;
            max_points = sourseTask.Topic.PointsPerTask;
            this.user_points = user_points;
        }

        public int topic_id { get; set; }
        public int type { get; set; }
        public int id { get; private set; }
        public string text { get; private set; }
        public string code { get; private set; }
        public int max_points { get; private set; }
        public int? user_points { get; private set; }


    }
}