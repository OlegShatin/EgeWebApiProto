using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.ApiViewModels
{
    public class AnswerViewModel
    {
        public AnswerViewModel(ExamTask sourseTask, int gotPoints)
        {
            task_id = sourseTask.Id;
            right_answer = sourseTask.Answer;
            max_points = sourseTask.Topic.PointsPerTask;
            type = sourseTask.Topic.IsShort ? 0 : 1;
            points = gotPoints;
        }

        public int max_points { get; set; }
        public int points { get; set; }
        public int type { get; set; }
        public int task_id { get; private set; }
        public string right_answer { get; private set; }


    }
}